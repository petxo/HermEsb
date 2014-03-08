from flask import Flask, render_template, jsonify, request
from ServiceStatistics import StatisticsView
from Environments import Environments
from Errores import ErroresRepository, ErroresView
from ServiceInfo import ServiceInfoView

app = Flask(__name__)

@app.route('/')
def Index():
    currentEnvironment = request.cookies.get('environment')
    if currentEnvironment is None:
        currentEnvironment = "DEV"
    envs = list()
    for e in Environments.GetEnvironments():
        envs.append(e.Name)
    return render_template("index.html", envs=envs, environment=currentEnvironment)

@app.route('/bus')
def BusInfo():
    currentEnvironment = request.cookies.get('environment')
    if currentEnvironment is None:
        currentEnvironment = "DEV"
    envs = list()
    for e in Environments.GetEnvironments():
        envs.append(e.Name)

    return render_template("bus.html", envs=envs, environment=currentEnvironment,
                           service=Environments.GetEnvironment(currentEnvironment).GetBusName())

@app.route('/errores/', methods=['GET', 'POST'])
def Errores():
    envs = list()
    for e in Environments.GetEnvironments():
        envs.append(e.Name)
    currentEnvironment = request.cookies.get('environment')
    if currentEnvironment is None:
        currentEnvironment = "DEV"
    return render_template("errores.html", envs=envs, environment=currentEnvironment)

@app.route('/geterrores/', methods=['GET', 'POST'])
@app.route('/geterrores/<entorno>/', methods=['GET', 'POST'])
def GetErrores(entorno="DEV"):
    page = 1
    rows = 20
    if not request.form.get('page') is None:
        page = int(request.form['page'])
    if not request.form.get('rows') is None:
        rows = int(request.form['rows'])
    view = ErroresView(Environments.GetEnvironment(entorno).MongoServer)

    filter = dict()
    if not request.form.get('Service') is None:
        filter['Service'] = request.form['Service']

    if not request.form.get('HandlerType') is None:
        filter['HandlerType'] = request.form['HandlerType']

    return jsonify(view.GetErrors(page, rows, filter))

@app.route('/geterror/', methods=['GET', 'POST'])
@app.route('/geterror/<entorno>/', methods=['GET', 'POST'])
def GetError(entorno="DEV"):
    view = ErroresView(Environments.GetEnvironment(entorno).MongoServer)
    id = 0
    if not request.form.get('id') is None:
        id = str(request.form['id'])
    return jsonify(view.GetError(id))

@app.route('/publish/<entorno>/', methods=['GET', 'POST'])
def Publish(entorno="DEV"):
    id = 0
    if not request.form.get('id') is None:
        id = str(request.form['id'])

    view = ErroresView(Environments.GetEnvironment(entorno).MongoServer)
    message = view.GetMessage(id)
    Environments.GetEnvironment(entorno).Publish(message)
    return jsonify({"Id": id})

@app.route('/publishto/<fromentorno>/<toentorno>/', methods=['GET', 'POST'])
def PublishTo(fromentorno="DEV", toentorno="DEV"):
    id = 0
    if not request.form.get('id') is None:
        id = str(request.form['id'])

    view = ErroresView(Environments.GetEnvironment(fromentorno).MongoServer)
    message = view.GetMessage(id)
    Environments.GetEnvironment(toentorno).Publish(message)
    return jsonify({"Id": id})

@app.route('/hangoutservices/<entorno>/', methods=['GET', 'POST'])
def HangoutServices(entorno="DEV"):
    view = ServiceInfoView(Environments.GetEnvironment(entorno).MongoServer)
    services = view.GetServicesNotResponding(90, 10)
    return jsonify({"Services": services})

@app.route('/memorytop/<entorno>/', methods=['GET', 'POST'])
def MemoryTop(entorno="DEV"):
    view = ServiceInfoView(Environments.GetEnvironment(entorno).MongoServer)
    services = view.MemoryTop(5)
    return jsonify({"Services": services})

@app.route('/latencytop/<entorno>/', methods=['GET', 'POST'])
def LatencyTop(entorno="DEV"):
    view = ServiceInfoView(Environments.GetEnvironment(entorno).MongoServer)
    services = view.LantecyTop(10)
    return jsonify({"Services": services})

@app.route('/latencypeaktop/<entorno>/', methods=['GET', 'POST'])
def LatencyPeakTop(entorno="DEV"):
    view = ServiceInfoView(Environments.GetEnvironment(entorno).MongoServer)
    services = view.LantecyPeakTop(10)
    return jsonify({"Services": services})

@app.route('/memoryservice/<entorno>/<id>', methods=['GET', 'POST'])
def MemoryServiceTop(entorno="DEV", id="Bus"):
    view = StatisticsView(Environments.GetEnvironment(entorno).MongoServer)
    services = view.GetMemory(id, 7200)
    return jsonify({"Data": services})

@app.route('/speedservice/<entorno>/<id>', methods=['GET', 'POST'])
def SpeedServiceData(entorno="DEV", id="Bus"):
    view = StatisticsView(Environments.GetEnvironment(entorno).MongoServer)
    services = view.GetSpeed(id, 7200)
    return jsonify({"Data": services})

@app.route('/latencyservice/<entorno>/<id>', methods=['GET', 'POST'])
def LatencyServiceData(entorno="DEV", id="Bus"):
    view = StatisticsView(Environments.GetEnvironment(entorno).MongoServer)
    services = view.GetLatency(id, 7200)
    return jsonify({"Data": services})

@app.route('/bandwidthservice/<entorno>/<id>', methods=['GET', 'POST'])
def BandWidthServiceData(entorno="DEV", id="Bus"):
    view = StatisticsView(Environments.GetEnvironment(entorno).MongoServer)
    services = view.GetBandWidth(id, 7200)
    return jsonify({"Data": services})

@app.route('/totalbandwidthservice/<entorno>/<id>', methods=['GET', 'POST'])
def TotalBandWidthServiceData(entorno="DEV", id="Bus"):
    view = StatisticsView(Environments.GetEnvironment(entorno).MongoServer)
    services = view.GetTotalBandWidth(id, 7200)
    return jsonify({services})

@app.route('/loadqueuetop/<entorno>/', methods=['GET', 'POST'])
def LoadQueueTop(entorno="DEV"):
    view = ServiceInfoView(Environments.GetEnvironment(entorno).MongoServer)
    services = view.LoadQueueTop(10)
    return jsonify({"Services": services})

if __name__ == '__main__':
    Environments.Create("environments.cfg")
    app.run(port=18045)
