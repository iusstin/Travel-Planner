import { Injectable } from "@angular/core";
import { baseUrl } from "../components/utils/constants";
import { User } from "../models/user";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";
import { Observable, catchError, throwError } from "rxjs";
import { Login } from "../models/login";
import { Register } from "../models/register";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private authUrl = `${baseUrl}/auth`;
    connectedUser: User | undefined;

    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    constructor(private http: HttpClient, private router: Router) { }

    private handleError(err: HttpErrorResponse) {
        let errorMessage = err.error.message;
        return throwError(() => new Error(errorMessage));
    }

    logInUser(loginData: Login): Observable<User> {
        return this.http.post<User>(`${this.authUrl}/login`, loginData, this.httpOptions).pipe(
            catchError(this.handleError)
        );
    }

    signUpUser(data: Register): Observable<any> {
        return this.http.post(`${this.authUrl}/register`, data, this.httpOptions).pipe(
            catchError(this.handleError)
        );
    }

    setConnectedUser(user: User) {
        this.connectedUser = user;
    }
    
    isUserLoggedIn(): boolean {
        return !!localStorage.getItem("token");
    }

    setToken(token: string) {
        localStorage.setItem('token', token);
    }

    getToken() {
        localStorage.getItem('token');
    }
}