import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventsComponent } from './events.component';
import { EventFormComponent } from './event-form/event-form.component';
import { EventDetailComponent } from './event-detail/event-detail.component';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { Page404Component } from '../page404/page404.component';

const routes: Routes = [
  { path: '', component: EventsComponent },
  { path: 'new', component: EventFormComponent },
  { path: 'edit/:id', component: EventFormComponent },
  { path: 'detail/:id', component: EventDetailComponent },
  { path: 'registrazione/:eventoId', component: RegistrationFormComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EventsRoutingModule {}
