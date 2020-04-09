import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { APIResponse } from './Models/APIResponse'

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent {
  public result: APIResponse;
  public isBusy: boolean;
  public filters: string[];

  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.filters = ['amsterdam'];

    this.makeCall();
  }

  ChangeFilters(filter: string) {
    if (!this.filters.includes(filter)) {
      this.filters.push(filter);
      console.log(this.filters, 'added:', filter);
    }
    else {
      this.filters.pop();
      console.log(this.filters, 'deleted:', filter);
    }

    this.makeCall();
  }

  makeCall() {
    this.result = null;
    this.http.get<APIResponse>(this.baseUrl + 'report/top/' + this.filters.join(",")).subscribe(result => {
      this.result = result;
    }, error => console.error(error));
  }
}


