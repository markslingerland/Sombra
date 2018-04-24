# Sombra

[![Build Status](https://travis-ci.org/markslingerland/Sombra.svg?branch=master)](https://travis-ci.org/markslingerland/Sombra)

Sombra is an open-source crowdfunding platform for charities.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them


* [Docker](https://www.docker.com/get-docker)

Without Docker:

* [.NET Core 2.1-Preview2](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.300-preview2)
* [RabbitMQ](https://www.rabbitmq.com/download.html)
* SQLEXPRESS running



### Installing

A step by step series of examples that tell you have to get a development env running

Say what the step will be

```
Open the solution and run the docker-compose up command

Reach the web solution on http://localhost:80
```

Or if you're not using docker

```
Make sure RabbitMQ is running and a SQLEXPRESS instance

dotnet run for the project
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

All the the unit tests are being run during build time.

## Deployment

With the complete repo on your server or pc you can use this command to start all the services.

```
docker-compose up
```

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Mark Slingerland** - *Initial work* - [markslingerland](https://github.com/markslingerland)
* **Jelle Kerkstra** - *Initial work* - [JelleKerkstra](https://github.com/JelleKerkstra)
* **Ruben Everwijn** - *Initial work* - [rubenv](https://github.com/rubenv)

See also the list of [contributors](https://github.com/markslingerland/sombra/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Acknowledgments

* Hat tip to anyone who's code was used
* Inspiration
* etc
