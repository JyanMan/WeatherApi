async function getData() {
    const url = "http://localhost:5154/api/weather/weather-forecast";
    try {
        const response = await fetch(url);
        if (!response.ok) 
            throw new Error(`Response status ${response.status}`);

        const jsonContent = await response.json;
        console.log(jsonContent);
    } catch (error) {
        console.error(error.message);
    }
}

getData();
