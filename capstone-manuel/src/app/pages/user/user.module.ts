import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditUserComponent } from './edit-user/edit-user.component';

@NgModule({
  declarations: [UserComponent, EditUserComponent],
  imports: [CommonModule, UserRoutingModule, ReactiveFormsModule, FormsModule],
})
export class UserModule {}
