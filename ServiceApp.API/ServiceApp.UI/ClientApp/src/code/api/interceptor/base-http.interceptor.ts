import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from "rxjs/operators";
import { ApiResult, ApiStatusCode, ApiErrorResult } from '..';
import { ConfigurationService } from '../../../code/services';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';


@Injectable()
export class BaseHttpInterceptor implements HttpInterceptor {

  constructor(
    private readonly configSvc: ConfigurationService,
    private dialog: MatDialog,
    ) {
  }

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const baseRequest: string = request.url;

    if (baseRequest.startsWith('api')) {
      request = request.clone({ url: this.configSvc.getApiUrl() + request.url });

      if (!request.headers.has('Content-Type')) {
        request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
      }

      request = request.clone({ headers: request.headers.set('Accept', 'application/json') });
    }

    return next.handle(request).pipe(
      map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse && baseRequest.startsWith('api')) {

          const result = new ApiResult(event.body);

          if (result.statusCode === ApiStatusCode.Success) {
            event = event.clone({ body: result.data });
            return event;
          }

          return this.handleError(event.body);
        }
        return event;
      }),
      catchError((error: HttpErrorResponse) => {
        if (error.status === 0) {
          return this.handleError({ statusCode: ApiStatusCode.EndpointOffline, message: 'Endpoint is offline' });
        }

        return throwError(error);
      }));
  }

  private handleError(response: any): Observable<any> {
        throw new ApiErrorResult(response);
  }
}
