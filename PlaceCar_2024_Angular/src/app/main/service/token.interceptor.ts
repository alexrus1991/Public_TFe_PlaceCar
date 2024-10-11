import {Injectable} from "@angular/core";
import {HttpEvent, HttpHandler, HttpRequest, HttpClient, HttpErrorResponse, HttpResponse, HttpSentEvent, HttpHeaderResponse, HttpProgressEvent, HttpUserEvent, HttpHeaders} from "@angular/common/http";
import {HttpInterceptor} from "@angular/common/http";
import { Router } from "@angular/router";

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
/*import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';*/

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  
  urlsToIgnore: Array<string> = ['login'];


  constructor(private router: Router ) {}

   intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> 
   {
      console.log('intercept');
       /*if (this.ignoreUri(req.url)) 
        {
          return next.handle(req);
        }*/

      let accessToken = sessionStorage.getItem('placecar.user-access-token');
      //console.log('accessToken');
      //console.log(accessToken);

      /*req.headers.set('Access-Control-Allow-Origin', '*');
      // Request methods you wish to allow
      req.headers.set('Access-Control-Allow-Methods', '*');
      // Request headers you wish to allow
      req.headers.set('Access-Control-Allow-Headers', '*');
      // Set to true if you need the website to include cookies in the requests sent
      // to the API (e.g. in case you use sessions)
      req.headers.set('Access-Control-Allow-Credentials', 'true');
        */

      var cloned = req;
       if (accessToken != "null" && accessToken != null) {

           cloned = req.clone({
               headers: req.headers
               .set('Authorization', 'Bearer ' + accessToken)
             });

             /*return next.handle(cloned).pipe(
              catchError(err => {
                  if (err instanceof HttpErrorResponse && err.error instanceof Blob && err.error.type === "application/json") {
                      // https://github.com/angular/angular/issues/19888
                      // When request of type Blob, the error is also in Blob instead of object of the json data
                      return new Promise<any>((resolve, reject) => {
                          let reader = new FileReader();
                          reader.onload = (e: Event) => {
                              try {
                                  const body = JSON.parse((<any>e.target).result);
                                  let message = body['message'];
                                  
                                  if(message?.includes("could not execute statement")) {
                                    body['message'] = message.split(']')[0].replace('[', '').replace('could not execute statement', '').trim();
                                  }

                                  console.log('error 3');
                                  console.log(err);
                                  console.log(body);
                                  reject(body);
                                  
                                  /*reject(new HttpErrorResponse({
                                      error: errmsg,
                                      headers: err.headers,
                                      status: err.status,
                                      statusText: err.statusText,
                                      url: err.url || undefined
                                  }));
                              } catch (e) {
                                console.log('error 2');
                              console.log(err);
                                  reject(err);
                              }
                          };
                          reader.onerror = (e) => {
                            console.log('error 0');
                            console.log(err);
                              reject(err);
                          };
                          reader.readAsText(err.error);
                      });
                  }
                  console.log('error 1');
                  console.log(err);
                  throw err;
              })
          );*/
           /*return next.handle(cloned).pipe(catchError(err => {

                if (err instanceof HttpErrorResponse) {
                    if (err.status === 401) {
                        this.router.navigate(['/main/auth/login']);
                    }
                }

               return throwError(err);
            }));*/
       }
       return next.handle(cloned).pipe(
        catchError(err => {
            if (err instanceof HttpErrorResponse && err.error instanceof Blob && err.error.type === "application/json") {
                // https://github.com/angular/angular/issues/19888
                // When request of type Blob, the error is also in Blob instead of object of the json data
                return new Promise<any>((resolve, reject) => {
                    let reader = new FileReader();
                    reader.onload = (e: Event) => {
                        try {
                            const body = JSON.parse((<any>e.target).result);
                            let message = body['message'];
                            
                            if(message?.includes("could not execute statement")) {
                              body['message'] = message.split(']')[0].replace('[', '').replace('could not execute statement', '').trim();
                            }

                            console.log('error 3');
                            console.log(err);
                            console.log(body);
                            reject(body);
                            /*
                            reject(new HttpErrorResponse({
                                error: errmsg,
                                headers: err.headers,
                                status: err.status,
                                statusText: err.statusText,
                                url: err.url || undefined
                            }));*/
                        } catch (e) {
                          console.log('error 2');
                        console.log(err);
                            reject(err);
                        }
                    };
                    reader.onerror = (e) => {
                      console.log('error 0');
                      console.log(err);
                        reject(err);
                    };
                    reader.readAsText(err.error);
                });
            }
            console.log('error 1');

            const error = err['error'];
            console.log(error);

            err['all'] = error;
            err['error'] = (typeof error === 'string' || error instanceof String) ? 
            error?.split('\r\n')[0]?.replace('System.Exception:', '').trim() : error;
            console.log(err);
            throw err;
        })
    );
       //return next.handle(req);
    }

    private ignoreUri(requestUrl: string): boolean 
    {
      var requestS = requestUrl.split('/');
      let str = requestS[requestS.length - 1];

      for (let address of this.urlsToIgnore) 
      {
        if (new RegExp(address).test(str)) 
        {
          return true;
        }
      }
     return false;
    }

}


