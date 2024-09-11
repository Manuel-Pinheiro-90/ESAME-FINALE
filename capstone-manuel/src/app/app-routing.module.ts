import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivateChild } from '@angular/router';
import { Page404Component } from './pages/page404/page404.component';
import { CharactersListComponent } from './pages/characters-list/characters-list.component';
import { AuthGuard } from './pages/auth/guard/auth.guard';
import { AdminGuard } from './pages/auth/guard/admin.guard';
import { NotAuthorizedComponent } from './pages/not-authorized/not-authorized.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full', // Reindirizza alla home
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./pages/auth/auth.module').then((m) => m.AuthModule), //rendere la home la pagina di arrivo e mettere l'auth in navbar
  },
  {
    path: 'home',
    loadChildren: () =>
      import('./pages/home/home.module').then((m) => m.HomeModule),
  },
  {
    path: 'events',
    loadChildren: () =>
      import('./pages/events/events.module').then((m) => m.EventsModule),
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
  },

  {
    path: 'user',
    loadChildren: () =>
      import('./pages/user/user.module').then((m) => m.UserModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'admin',
    loadChildren: () =>
      import('./pages/admin/admin.module').then((m) => m.AdminModule),
    canActivate: [AdminGuard],
  },
  { path: 'personaggi', component: CharactersListComponent },
  {
    path: 'not-authorized',
    component: NotAuthorizedComponent,
  },

  {
    path: '**',
    component: Page404Component,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
