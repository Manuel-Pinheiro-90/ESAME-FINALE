import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EventsRoutingModule } from './events-routing.module';
import { EventsComponent } from './events.component';
import { EventFormComponent } from './event-form/event-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    EventsComponent,
    EventFormComponent
  ],
  imports: [
    CommonModule,
    EventsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
  ]
})
export class EventsModule { }
