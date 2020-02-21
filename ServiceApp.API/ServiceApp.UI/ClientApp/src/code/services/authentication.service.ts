import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { User } from '../../code/models';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  constructor(private http: HttpClient) { }

  login(user: User) {
    return this.http.post<any>('api/users/authenticate', JSON.stringify(user))
      .pipe(map(res => {

        if (res.user) {
          let user = res.user;
          user.authdata = res.token;
          localStorage.setItem('currentUser', JSON.stringify(user));
        }

        return user;
      }));
  }

  register(user: User) {
    return this.http.post<any>('api/users/register', JSON.stringify(user))
      .pipe(map(res => {

        if (res.user) {
          let user = res.user;
          user.authdata = res.token;
          localStorage.setItem('currentUser', JSON.stringify(user));
        }

        return user;
      }));
  }

  logout() {
    localStorage.removeItem('currentUser');
  }
}
