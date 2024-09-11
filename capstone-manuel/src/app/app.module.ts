import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from './main-component/navbar/navbar.component';
import { FooterComponent } from './main-component/footer/footer.component';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthInterceptor } from './pages/auth/auth.interceptor';
import { Page404Component } from './pages/page404/page404.component';
import { CharactersListComponent } from './pages/characters-list/characters-list.component';
import { NotAuthorizedComponent } from './pages/not-authorized/not-authorized.component';

@NgModule({
  declarations: [AppComponent, NavbarComponent, FooterComponent, Page404Component, CharactersListComponent, NotAuthorizedComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,

    HttpClientModule,
  ],

  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
