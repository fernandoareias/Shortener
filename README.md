# Shortener

This innovative project represents a highly performant URL shortener developed in the latest version of the .NET 6 framework. With a robust and modern architecture, it incorporates technologies such as Redis, MongoDB, and advanced monitoring tools to provide an efficient and scalable URL shortening experience.

### Key Features:

1. **Cutting-Edge Technology:**

   - Developed using the latest version of the .NET 6 framework, ensuring access to the latest features and performance improvements.

2. **NoSQL Database:**

   - Integration with MongoDB for efficient and flexible data storage, providing a scalable structure to manage information related to URLs.

3. **Fast Data Caching:**

   - Utilization of Redis for caching, optimizing access to shortened URLs and accelerating system response.

4. **Comprehensive Unit Testing:**

   - Implementation of robust unit tests, ensuring code reliability and facilitating future updates and modifications with confidence.

5. **Advanced Monitoring:**

   - Integration of Prometheus and Grafana for creating observability dashboards. Export of metrics directly from MongoDB, providing a comprehensive view of application performance.

6. **Docker Support:**
   - Efficient implementation of Docker containers, simplifying deployment and ensuring consistency in the execution environment.

### Benefits:

- **High Performance:**

  - The strategic combination of Redis and MongoDB results in fast response times, providing an agile and effective URL shortening experience.

- **Scalability:**

  - The architecture is designed with scalability in mind, allowing the system to grow according to demands while maintaining consistent performance.

- **Observability:**

  - Grafana dashboards offer detailed insights into the internal workings of the system, enabling proactive adjustments and ensuring smooth operation.

- **Ease of Deployment:**
  - Docker container support significantly simplifies the deployment process, providing flexibility and consistency in the production environment.

### Next Steps:

This project not only delivers a robust solution for URL shortening but also exemplifies a commitment to development best practices, scalability, observability, and code quality, encompassing comprehensive unit tests. Future steps may involve implementing additional features, such as tracking URL shortening by company for calculating the cost per each shortened link. Additionally, a dashboard will be developed to visualize this monthly cost, along with the possibility of introducing a rate-limiting mechanism to restrict the number of requests a company can make per month. Continuous optimizations will be made based on community feedback.

# How i can run?

Its simple, just clone the project and run

`docker-compose up --build`

# Applications

| Application    | Host                   |
| :------------- | :--------------------- |
| API            | http://localhost:5009  |
| MongoDB        | http://localhost:27017 |
| Redis          | http://localhost:6379  |
| Prometheus     | http://localhost:9090  |
| Mongo exporter | http://localhost:9216  |
| Grafana        | http://localhost:3000  |

# Grafana

| User  | Password |
| :---- | :------- |
| admin | @admin   |

## Dashboards

### API

<img width="1439" alt="Screenshot 2024-01-02 at 22 25 37" src="https://github.com/fernandoareias/Shortener/assets/87771786/b13ff4e7-b12a-4109-a036-ba21a0873736">

### Mongo DB

<img width="1440" alt="Screenshot 2024-01-02 at 22 13 55" src="https://github.com/fernandoareias/Shortener/assets/87771786/405c5528-a478-4c68-bb92-16afd49ceb6b">
