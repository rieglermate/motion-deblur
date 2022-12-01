from fastapi.testclient import TestClient

from .main import app

client = TestClient(app)

# A test image from the HIDE dataset converted to base64 using https://www.base64-image.de/
with open("./app/valid_base64_64fromGOPR0965.txt") as f:
    image_data = f.read()


def test_root_redirect_check():
    response = client.get("/", allow_redirects=False)
    assert response.is_redirect

def test_root_redirect_docs():
    response = client.get("/", allow_redirects=True)
    assert response.url == client.base_url + "/docs"

def test_invalid_url():
    response = client.get("/nonexistent")
    assert response.status_code == 404

def test_valid_request():
    response = client.post("/deblur/", json={"imageData": image_data})
    assert response.status_code == 200
    response_data = response.json()
    assert list(response_data.keys()) == ["imageResize", "imageDeblur"]

def test_invalid_image():
    response = client.post("/deblur/", json={"imageData": image_data.replace("e", "f")})
    assert response.status_code == 400

def test_invalid_data():
    response = client.post("/deblur/", json={"imageData": image_data[:-2]})
    assert response.status_code == 400

def test_invalid_request_get():
    response = client.get("/deblur/", json={"imageData": image_data})
    assert response.status_code == 405
    assert response.json()["detail"] == "Method Not Allowed"
