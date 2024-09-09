import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { AdminRegistrazioniComponent } from './admin-registrazioni/admin-registrazioni.component';

@NgModule({
  declarations: [AdminComponent, AdminRegistrazioniComponent],
  imports: [CommonModule, AdminRoutingModule],
})
export class AdminModule {}
