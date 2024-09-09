import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AdminRegistrazioniComponent } from './admin-registrazioni/admin-registrazioni.component';

const routes: Routes = [
  { path: '', component: AdminComponent },
  { path: 'registrazioni', component: AdminRegistrazioniComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
