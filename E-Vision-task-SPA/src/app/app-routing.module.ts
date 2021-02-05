import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductModule } from './Products/product.module';



const routes: Routes = [
  {path: '', redirectTo: '/products', pathMatch: 'full'},
  {
    path: 'products', loadChildren: () => ProductModule
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
