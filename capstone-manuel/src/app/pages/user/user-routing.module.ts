import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { CreatePersonaggioComponent } from './create-personaggio/create-personaggio.component';
import { RegisterDetailComponent } from './register-detail/register-detail.component';
import { PgDetailComponent } from './pg-detail/pg-detail.component';
import { PaymentComponent } from './payment/payment.component';

const routes: Routes = [
  { path: '', component: UserComponent },
  { path: 'user', component: UserComponent },
  { path: 'editProfile', component: EditUserComponent },
  { path: 'create-personaggio', component: CreatePersonaggioComponent },
  { path: 'registrazioni/:id', component: RegisterDetailComponent },
  { path: 'personaggio/:id', component: PgDetailComponent },
  { path: 'pagamento', component: PaymentComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserRoutingModule {}
