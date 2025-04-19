async function getData() {
    const location = document.getElementById("location").value;
    if (!location || location === '') {
        throw new Error("please input a location");
    }
    console.log(location);
    const url = `http://localhost:5154/api/weather/weather-forecast/current-conditions?location=${location}`;

    try {
        const response = await fetch(url);
        if (!response.ok) 
            throw new Error(`Response status ${response.status}`);

        const jsonContent = await response.json();

        if (jsonContent) {
            UpdateCurrConditions(jsonContent);
        }

    } catch (error) {
        console.error(error.message);
    }
}

function UpdateCurrConditions(currentConditions) {
    const conditionLabel = document.getElementById("condition");
    const humidityLabel = document.getElementById("humidity");
    const feelslikeLabel = document.getElementById("feelslike");
    const temperatureLabel = document.getElementById("temperature");
    const pressureLabel = document.getElementById("pressure");

    console.log(currentConditions);
    conditionLabel.innerText = currentConditions.conditions;
    humidityLabel.innerText = currentConditions.humidity;
    feelslikeLabel.innerText = currentConditions.feelslike;
    pressureLabel.innerText = currentConditions.pressure;
    temperatureLabel.innerText = currentConditions.temp;
}
