__author__ = 'Sergio'

from flask import Flask
from flask.ext.login import LoginManager

__author__ = 'Sergio'

app = Flask(__name__)
login_manager = LoginManager()
login_manager.init_app(app)
login_manager.login_view = 'login'