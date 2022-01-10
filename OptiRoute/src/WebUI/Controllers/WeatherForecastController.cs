﻿using Microsoft.AspNetCore.Mvc;
using OptiRoute.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OptiRoute.WebUI.Controllers
{
    public class WeatherForecastController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }
    }
}