import { Routes } from '@angular/router';
import { CalendarComponent } from './calendar/calendar.component';
import {HomeComponent} from './home/home.component'
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    {path : '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'calendar', component: CalendarComponent},
            {path: 'dashboard', component: HomeComponent}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];