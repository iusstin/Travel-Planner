import { Injectable } from "@angular/core";
import { baseUrl } from "../components/utils/constants";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Place } from "../models/place";
import { Observable, catchError, throwError } from "rxjs";

@Injectable({
    providedIn: 'root'
  })
  export class PlaceService {
    private placesUrl = `${baseUrl}/places`;
  
    httpOptions = {
      headers: new HttpHeaders({'Content-Type':'application/json'})
    }
  
    constructor(private http: HttpClient) {}
  
    addNewPlace(place: Place) : Observable<Place> {
      return this.http.post<Place>(`${this.placesUrl}/add`, place, this.httpOptions).pipe(
        catchError(this.handleError)
      );
    }
  
    private handleError(err: HttpErrorResponse) {
      let errorMessage = "";
      return throwError(() => new Error(errorMessage));
    }
  }