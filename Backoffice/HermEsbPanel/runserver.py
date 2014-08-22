import os
from flask import session, request
import sys
from os.path import dirname

pathfile = dirname(os.path.abspath(__file__))
projectRoot = dirname(os.path.abspath(pathfile))
sys.path.append(projectRoot)


def root_dir():  # pragma: no cover
    return os.path.abspath(os.path.dirname(__file__))


def get_file(filename):  # pragma: no cover
    try:
        src = os.path.join(root_dir(), filename)
        return open(src).read()
    except IOError as exc:
        return str(exc)

from G3ControlPanel.Controllers import HomeController, SeguimientoController, StatisticsController, EnviosController,IntegracionG2Controller, ValoracionController, SecurityController

Settings.create(os.path.join(root_dir(), "settings.cfg"))
MongoRepository.Create(Settings.instance().Config)
EndPoints.Publishers.Create(Settings.instance().Config)
Environment.Create(Settings.instance().Config)


class ApplicationConfig(object):
    DEBUG = False
    TESTING = False
    SECRET_KEY = "eldelmediodeloschichos"


@app.before_request
def before_request():
    if not 'enviroment' in session:
        if 'enviroment' in request.cookies:
            session['enviroment'] = request.cookies.get('enviroment')
        else:
            session['enviroment'] = "DEV"

    if not 'enviromentlist' in session:
        session['enviromentlist'] = list()
        for env in Settings.instance().Config['enviroments']:
            session['enviromentlist'].append({'name': env['name'], 'secure': env['secure']})



@app.route('/static/css/<file>')
def static_css(file):
    complete_path = os.path.join(root_dir(), 'static/css/' + file)
    rv = app.make_response(get_file(complete_path))
    rv.mimetype = 'text/css'
    return rv


@app.route('/static/css/<directory>/<file>')
def static_css2(directory, file):
    complete_path = os.path.join(root_dir(), 'static/css/' + directory + '/' + file)
    rv = app.make_response(get_file(complete_path))
    rv.mimetype = 'text/css'
    return rv


@app.route('/static/css/<directory>/<directory2>/<file>')
def static_css3(directory, directory2, file):
    complete_path = os.path.join(root_dir(), 'static/css/' + directory + '/' + directory2 + '/' + file)
    rv = app.make_response(get_file(complete_path))
    rv.mimetype = 'text/css'
    return rv


@app.route('/static/css/<directory>/<directory2>/<directory3>/<file>')
def static_css4(directory, directory2, directory3, file):
    complete_path = os.path.join(root_dir(), 'static/css/' + directory + '/' + directory2 + '/' + directory3 + '/' + file)
    rv = app.make_response(get_file(complete_path))
    rv.mimetype = 'text/css'
    return rv


@app.route('/static/js/<file>')
def static_js(file):
    complete_path = os.path.join(root_dir(), 'static/js/' + file)
    rv = app.make_response(get_file(complete_path))
    rv.mimetype = 'application/javascript'
    return rv


@app.route('/static/js/<directory>/<file>')
def static_js2(directory, file):
    complete_path = os.path.join(root_dir(), 'static/js/' + directory + '/' + file)
    rv = app.make_response(get_file(complete_path))
    rv.mimetype = 'application/javascript'
    return rv


@app.route('/static/js/<directory>/<directory2>/<file>')
def static_js3(directory, directory2, file):
    complete_path = os.path.join(root_dir(), 'static/js/' + directory + '/' + directory2 + '/' + file)
    rv = app.make_response(get_file(complete_path))
    rv.mimetype = 'application/javascript'
    return rv


if __name__ == '__main__':
    app.config.from_object(ApplicationConfig)
    app.run(host='0.0.0.0', port=8100, debug=True)

