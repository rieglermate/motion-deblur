"""
Separate module for defining the model used in the API.
"""

from keras.models import Model
from keras.layers import Input, Conv2D, LeakyReLU, MaxPooling2D, Conv2DTranspose, concatenate

width, height = 256, 144


def create_model():
    """Assemble the U-Net neural network."""
    inp_shape = (width, height, 3)
    filter_size = 64

    def conv_block(inp, i):
        conv = Conv2D(filter_size * (2**i), (3, 3), padding='same')(inp)
        conv = LeakyReLU()(conv)
        conv = Conv2D(filter_size * (2**i), (3, 3), padding='same')(conv)
        conv = LeakyReLU()(conv)
        pool = MaxPooling2D((2, 2))(conv)
        return pool, conv

    def bottleneck(inp, i):
        conv = Conv2D(filter_size * (2**i), (3, 3), padding='same')(inp)
        conv = LeakyReLU()(conv)
        conv = Conv2D(filter_size * (2**i), (3, 3), padding='same')(conv)
        conv = LeakyReLU()(conv)
        return conv

    def deconv_block(inp, shortcut, i):
        deconv = Conv2DTranspose(filter_size * (2**i), (3, 3), strides=(2, 2), padding="same")(inp)
        uconv  = concatenate([shortcut, deconv])
        uconv  = Conv2D(filter_size * (2**i), (3, 3), padding="same")(uconv)
        uconv  = LeakyReLU()(uconv)
        uconv  = Conv2D(filter_size * (2**i), (3, 3), padding="same")(uconv)
        uconv  = LeakyReLU()(uconv)
        return uconv

    inp = Input(inp_shape)
    conv0, pre_pool0 = conv_block(inp,   0)
    conv1, pre_pool1 = conv_block(conv0, 1)
    conv2, pre_pool2 = conv_block(conv1, 2)
    conv3, pre_pool3 = conv_block(conv2, 3)
    mid              = bottleneck(conv3, 4)
    deconv3          = deconv_block(mid,     pre_pool3, 3)
    deconv2          = deconv_block(deconv3, pre_pool2, 2)
    deconv1          = deconv_block(deconv2, pre_pool1, 1)
    deconv0          = deconv_block(deconv1, pre_pool0, 0)
    out = Conv2D(3, (1, 1), activation='sigmoid')(deconv0)
    model = Model(inputs=[inp], outputs=[out])
    return model

def load_model_256x144():
    """Load the stored weights."""
    model = create_model()
    model.load_weights("./app/model_weights.hdf5")
    return model
