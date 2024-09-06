import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Page404Component } from './pages/page404/page404.component';
import { CharactersListComponent } from './pages/characters-list/characters-list.component';

const routes: Routes = [
  {
    path: '',
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
  },

  {
    path: 'user',
    loadChildren: () =>
      import('./pages/user/user.module').then((m) => m.UserModule),
  },
  {
    path: 'admin',
    loadChildren: () =>
      import('./pages/admin/admin.module').then((m) => m.AdminModule),
  },
  { path: 'personaggi', component: CharactersListComponent },
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
