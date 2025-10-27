@model AccuWeathers.Models.CityWeatherModel
@{
    ViewData["Title"] = "City forecast";
}

< h2 > Forecast for the selected city </ h2 >
< h2 > @Html.DisplayFor(model => model.CityName) </ h2 >

< div class= "row text-dark" >
    < div class= "col-sm-2" >
        Temperature(Celsius):
    </ div >
    < div class= "col-sm-10" >
        @Html.DisplayFor(model => model.TempMinCelsius) °C
    </ div >

    < div class= "col-sm-2" >
        Temperature(Celsius):
    </ div >
    < div class= "col-sm-10" >
        @Html.DisplayFor(model => model.TempMaxCelsius) °C
    </ div >

    < div class= "col-sm-2" >
        Severity:
    </ div >
    < div class= "col-sm-10" >
        @Html.DisplayFor(model => model.Severity)
    </ div >

    < div class= "col-sm-2" >
        Category:
    </ div >
    < div class= "col-sm-10" >
        @Html.DisplayFor(model => model.Category)
    </ div >

    < div class= "col-sm-2" >
        Weather Text:
    </ div >
    < div class= "col-sm-10" >
        @Html.DisplayFor(model => model.WeatherText)
    </ div >
</ div >
