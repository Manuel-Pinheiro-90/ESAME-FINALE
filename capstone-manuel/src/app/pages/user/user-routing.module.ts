import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { CreatePersonaggioComponent } from './create-personaggio/create-personaggio.component';
import { RegisterDetailComponent } from './register-detail/register-detail.component';

const routes: Routes = [
  { path: '', component: UserComponent },
  { path: 'user', component: UserComponent },
  { path: 'editProfile', component: EditUserComponent },
  { path: 'create-personaggio', component: CreatePersonaggioComponent },
  { path: 'registrazioni/:id', component: RegisterDetailComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserRoutingModule {}
