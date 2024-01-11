import { Component } from '@angular/core';
import { WeatherForecastsClient, WeatherForecast } from '../web-api-client';
import {BehaviorSubject} from "rxjs";

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];

  public loading$: BehaviorSubject<boolean> = new BehaviorSubject(false);
  public filters = {
    weather: "",
    temp: null
  };

  constructor(private client: WeatherForecastsClient) {
    this.refresh();
  }

  public filterChanged() {
    this.refresh();
  }

  private refresh() {
    this.loading$.next(true);
    this.client.getWeatherForecasts(this.filters.weather, this.filters.temp || null).subscribe(
      result => this.forecasts = result,
      error => console.error(error),
      () => this.loading$.next(false)
    );
  }
}
