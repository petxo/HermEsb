from distutils.core import setup

setup(
    name='HermEsbBalancer',
    version='1.0.0',
    packages=['hermEsbBalancer'],
    url='',
    license='',
    author='Sergio',
    author_email='',
    description='Balanceador del HermEsb',
    extras_require={
        'Channels': ["pika>=0.9.8"]
    }
)
