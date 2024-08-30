import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventsComponent } from './events.component';
import { EventFormComponent } from './event-form/event-form.component';

const routes: Routes = [
  { path: '', component: EventsComponent },
  { path: 'new', component: EventFormComponent },
  { path: 'edit/:id', component: EventFormComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EventsRoutingModule {}
