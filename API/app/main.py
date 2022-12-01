"""
Functions defining the API.
"""
import io
import base64
import binascii

from fastapi import FastAPI, HTTPException
from fastapi.responses import RedirectResponse
from pydantic import BaseModel

import numpy as np

from PIL import Image, UnidentifiedImageError

from .model import load_model_256x144

responses = {
    400: {
        "description": "Bad Request",
        "content": {
            "application/json": {
                "example": {
                    "detail": "Sent base64 data is invalid"
                }
            }
        }
    },
    404: {
        "description": "Item Not Found",
        "content": {
            "application/json": {}
        }
    },
    413: {
        "description": "Payload Too Large",
        "content": {
            "application/json": {
                "example": {
                    "detail": "Too much data (> ~50MB)"
                }
            }
        }
    },
}

app = FastAPI(
    title="Motion Deblur API",
    version="0.1.0",
    description="This API tries to deblur motion blurred images in 256x144 resolution using a Tensorflow model.",
    contact= {
        "name": "Riegler Máté",
        "email": "rmate@student.elte.hu",
    }
)

model = load_model_256x144()


class ImageRequest(BaseModel):
    """Incoming format"""
    imageData: str

class ImageResponse(BaseModel):
    """Outgoing format"""
    imageResize: str
    imageDeblur: str


@app.get("/")
def get_root():
    """Redirects to the generated OpenAPI docs of this API."""
    return RedirectResponse("/docs")

def apply_model(image: Image) -> Image:
    """Apply the loaded tf model on a PIL Image object."""
    model_input = np.asarray(image, dtype=np.float32)/255
    model_input = model_input.transpose((1, 0, 2))
    model_input = model_input.reshape((1, 256, 144, 3))

    model_result = model(model_input)

    result_array = np.asarray(model_result, dtype=np.float32)
    result_array = result_array.reshape((256, 144, 3))
    result_array = result_array.transpose((1, 0, 2))
    result_img: Image = Image.fromarray(np.uint8(result_array*255))
    return result_img

def image_to_bytes(image: Image) -> bytes:
    """Convert PIL Image object to bytes correctly."""
    bytes_buffer = io.BytesIO()
    image.save(bytes_buffer, "PNG")
    bytes_buffer.seek(0)
    image_bytes = bytes_buffer.read()
    return image_bytes

def base64_to_image(img_data: str) -> Image:
    """Convert Base64 encoded string to Image object."""
    img_bytes: bytes = base64.b64decode(img_data)
    img: Image = Image.open(io.BytesIO(img_bytes))
    return img

def image_to_base64(img: Image) -> str:
    """Convert Image object to Base64 encoded string."""
    img_bytes: bytes = image_to_bytes(img)
    img_data: str = base64.b64encode(img_bytes).decode()
    return img_data

@app.post("/deblur/", response_model=ImageResponse, responses=responses)
def deblur_image(request: ImageRequest):
    """Convert the incoming Base64 encoded string to Image and resize it to (256,144),\
        apply the deblurring model then send the response as JSON/dictionary."""
    data: str = request.imageData
    if len(data) > 70_000_000:
        raise HTTPException(413, detail="Too much data (> ~50MB)")

    try:
        img: Image = base64_to_image(request.imageData)
    except binascii.Error as exc:
        raise HTTPException(400, detail="Sent base64 data is invalid") from exc
    except UnidentifiedImageError as exc:
        raise HTTPException(400, detail="Sent base64 data is not an image") from exc
    
    # this is only needed because Image.open() is a lazy operation
    # img.verify() is not enough to verify as valid image
    # as found here https://github.com/python-pillow/Pillow/issues/3012
    # if the data is an invalid image, Image.resize() raises OSError because it internally calls load() anyway
    try:
        img.load()
    except OSError as exc:
        raise HTTPException(400, detail="Sent base64 data is an invalid image") from exc

    # here we can assume img is valid image
    img_resized: Image = img.resize((256, 144), resample=Image.Resampling.LANCZOS).convert("RGB")
    result_img: Image = apply_model(img_resized)

    return {
        "imageResize": image_to_base64(img_resized),
        "imageDeblur": image_to_base64(result_img)
        }
