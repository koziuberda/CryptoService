# My Application

## General Information

This application aggregates prices for cryptocurrencies from three exchanges: Coinbase, Bybit, and Binance. 
We use `coinapi.io` as our data provider to ensure reliable and accurate price updates. 
Prices are updated in real time, providing users with the most current market information.

## Technical Comments

### Queue Processing with Timer

Our application implements a queue processing system using a timer. This approach prevents excessive database requests, thereby reducing system load. The timer triggers queue processing at regular intervals, ensuring an even distribution of requests.

### Task Management with Semaphore

To prevent system overload from processing tasks, we use `SemaphoreSlim`. The semaphore limits the number of tasks that can run concurrently, ensuring stable and predictable application performance. This helps avoid excessive resource usage and enhances overall system efficiency.

## Steps to Run the App

1. Open a terminal in the root directory of your CryptoService project.
2. Run the following commands:

    ```sh
    docker-compose build
    docker-compose up
    ```

These commands will build and start the application using Docker.
Once the application is ready, you can access the API documentation via Swagger at [http://localhost:8000/swagger/index.html](http://localhost:8000/swagger/index.html).