import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditUserComponent } from './edit-user/edit-user.component';
import { CreatePersonaggioComponent } from './create-personaggio/create-personaggio.component';
import { RegisterDetailComponent } from './register-detail/register-detail.component';

@NgModule({
  declarations: [UserComponent, EditUserComponent, CreatePersonaggioComponent, RegisterDetailComponent],
  imports: [CommonModule, UserRoutingModule, ReactiveFormsModule, FormsModule],
})
export class UserModule {}
