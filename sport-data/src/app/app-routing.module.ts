import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './core/home/home.component';
import { PageComponent } from './page/page.component';
import { authGuard } from './shared/guards/auth.guard';

const routes: Routes = [
  {
    path: 'page',
    component: PageComponent,
    canActivate: [authGuard]
  },
  {
    path: '',
    pathMatch: 'full',
    component: HomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
