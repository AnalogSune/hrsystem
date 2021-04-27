import { Routes } from '@angular/router';
import { CalendarComponent } from './calendar/calendar.component';
import { CompanyComponent } from './company/company.component';
import {HomeComponent} from './home/home.component'
import { ProfileViewerComponent } from './profile-viewer/profile-viewer.component';
import { RequestsComponent } from './requests/requests.component';
import { DepartmentsComponent } from './_admin/departments/departments.component';
import { RegisterComponent } from './_admin/register/register.component';
import { AdminAuthGuard, AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    {path : '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'calendar', component: CalendarComponent},
            {path: 'dashboard', component: HomeComponent},
            {path: 'requests', component: RequestsComponent},
            {path: 'profile', component: ProfileViewerComponent},
            {path: 'company', component: CompanyComponent}
        ]
    },
    {
        path: 'admin',
        runGuardsAndResolvers: 'always',
        canActivate: [AdminAuthGuard],
        children: [
            {path: 'register', component: RegisterComponent},
            {path: 'departments', component: DepartmentsComponent}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];