import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import CryptoJS from 'crypto-js'

import { environment } from './../../../environments/environment';

import { catchError, of, throwError } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserService } from '../api/generated';
import { MessageService } from 'primeng/api';

interface PlaceCarUser { 
    sub: string;
    role: string;
    roles: string[];
    emailaddress: string;
    name: string;
    nameIdentifier: string;
}

@Injectable({
    providedIn: 'root',
})

export class AuthService {

    public jwtHelper: JwtHelperService = new JwtHelperService();

    constructor(private http: HttpClient, 
        private userService: UserService) {

    }

    login(username: string, password: string, onResponse: Function = null, onError: Function = null) {

        //var sha1 = CryptoJS.SHA1(password.trim());
        /*var sha1 = password.trim();
        var wordArray = CryptoJS.enc.Utf8.parse(username.trim() + ":" + sha1);
        var base64 = CryptoJS.enc.Base64.stringify(wordArray);
        localStorage.setItem('placecar.login', base64);
        */

        this.userService.apiUsersLoginPost(username, password).subscribe({
            next: (result) => {
                let decode = this.jwtHelper.decodeToken(result.token);

                console.log(decode);
                let roles = [];
                let role = decode['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
                if(Array.isArray(role)) {
                    roles = roles.concat(role);
                }
                else {
                    roles.push(role);
                }

                const placeCarUser = {
                    sub: decode['sub'],
                    //role: role,
                    roles: roles,
                    emailaddress: decode['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
                    name: decode['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
                    nameIdentifier: decode['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']
                } as PlaceCarUser;

                sessionStorage.setItem('placecar.user-access-token', result.token);
                //sessionStorage.setItem('placecar.user', JSON.stringify(placeCarUser));

                console.log(placeCarUser);
                onResponse(placeCarUser);
            },
            error: (err) => {
                onError(err);
            },
        });


    }

    logout() {
        //console.log('logout');
        sessionStorage.setItem('placecar.user', null);
        sessionStorage.setItem('placecar.user-access-token', null);
    }

    public isLoggedIn(): boolean {
        
        let user = sessionStorage.getItem("placecar.user");
       
        return ((user && user.length > 0 && user != 'null' && !this.isTokenExpired()) || !this.isTokenExpired());
    }

    public getUser(): object {
        //console.log('getUser');
        let user: string = sessionStorage.getItem("placecar.user");
        if (user && user.length > 0 && user != 'null' ) {
            return JSON.parse(user);
        }
        return null;
    }

    private isTokenExpired(): boolean {
        //console.log('isTokenExpired');

        let user = sessionStorage.getItem('placecar.user');
        if (user && user.length > 0 && user != 'null' ) {

            let accessToken = sessionStorage.getItem('placecar.user-access-token');
            let decode = this.jwtHelper.decodeToken(accessToken);

            //console.log("ex: " + (new Date().getTime() >  decode['exp'] * 1000));
            return new Date().getTime() >  decode['exp'] * 1000;
            //return this.jwtHelper.isTokenExpired(accessToken);
        }
        return true;
    }
}
