import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authSvc: AuthService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const accessData = this.authSvc.getAccessData();
    console.log('Adding Authorization header with token:', accessData);
    if (!accessData) return next.handle(request);

    console.log('Adding Authorization header with token:', accessData.token);

    const newReq = request.clone({
      headers: request.headers.append(
        'Authorization',
        `Bearer ${accessData.token}`
      ),
    });

    return next.handle(newReq);
  }
}
