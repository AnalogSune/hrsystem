import { Routes } from '@angular/router';
import { CalendarComponent } from './calendar/calendar.component';
import { CompanyComponent } from './company/company.component';
import { CvComponent } from './cv/cv.component';
import { DocumentsComponent } from './documents/documents.component';
import {HomeComponent} from './home/home.component'
import { ProfileViewerComponent } from './profile-viewer/profile-viewer.component';
import { RequestsComponent } from './requests/requests.component';
import { DepartmentsComponent } from './_admin/departments/departments.component';
import { RecruitmentComponent } from './_admin/recruitment/recruitment.component';
import { RegisterComponent } from './_admin/register/register.component';
import { ShiftsComponent } from './_admin/shifts/shifts.component';
import { AdminAuthGuard, AuthGuard } from './_guards/auth.guard';
import { TasksComponent } from './tasks/tasks.component';
import { MeetingsComponent } from './meetings/meetings.component';

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
            {path: 'company', component: CompanyComponent},
            {path: 'documents', component: DocumentsComponent},
            {path: 'tasks', component: TasksComponent},
            {path: 'meetings', component: MeetingsComponent}
        ]
    },
    {
        path: 'admin',
        runGuardsAndResolvers: 'always',
        canActivate: [AdminAuthGuard],
        children: [
            {path: 'register', component: RegisterComponent},
            {path: 'departments', component: DepartmentsComponent},
            {path: 'shifts', component: ShiftsComponent},
            {path: 'recruitment', component: RecruitmentComponent}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];