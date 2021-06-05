import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs'
import { CommonModule, DatePipe } from '@angular/common';
import { JwtModule } from '@auth0/angular-jwt';
import {LayoutModule} from '@angular/cdk/layout';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { MatTooltipModule } from '@angular/material/tooltip'
import { RouterModule } from '@angular/router';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { SideBarComponent } from './side-bar/side-bar.component';
import { appRoutes } from './routes';
import { HomeComponent } from './home/home.component';
import { SearchBarComponent } from './search-bar/search-bar.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { CalendarComponent } from './calendar/calendar.component';
import { PostsComponent } from './posts/posts.component';
import { TimePipe } from './_pipes/time.pipe';
import { RequestsComponent } from './requests/requests.component';
import { ProfileViewerComponent } from './profile-viewer/profile-viewer.component';
import { RegisterComponent } from './_admin/register/register.component';
import { DepartmentsComponent } from './_admin/departments/departments.component';
import { CompanyComponent } from './company/company.component';
import { DocumentsComponent } from './documents/documents.component';
import { ShiftsComponent } from './_admin/shifts/shifts.component';
import { CvComponent } from './cv/cv.component';
import { RecruitmentComponent } from './_admin/recruitment/recruitment.component';
import { TasksComponent } from './tasks/tasks.component';
import { MeetingsComponent } from './meetings/meetings.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {MatSelectModule} from '@angular/material/select';
import {MatRadioModule} from '@angular/material/radio';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatListModule} from '@angular/material/list';
import {MatInputModule} from '@angular/material/input';
import {MatTabsModule} from '@angular/material/tabs';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MeetingsTableComponent } from './meetings-table/meetings-table.component';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatTableModule} from '@angular/material/table';
import {MatRippleModule} from '@angular/material/core';
import {MatBadgeModule} from '@angular/material/badge';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatCardModule} from '@angular/material/card';
import {MatSortModule} from '@angular/material/sort';
import {MatDialogModule} from '@angular/material/dialog';
import { CreateTaskComponent } from './create-task/create-task.component';
import { CreateMeetingComponent } from './create-meeting/create-meeting.component';
import { CreateDepartmentComponent } from './_admin/create-department/create-department.component';
import { CreateShiftComponent } from './_admin/create-shift/create-shift.component';
import { RecruitmentAdminnotesComponent } from './_admin/recruitment-adminnotes/recruitment-adminnotes.component';
import { RecruitmentCoverletterComponent } from './_admin/recruitment-coverletter/recruitment-coverletter.component';
import { ReactiveFormsModule } from '@angular/forms';
import {MatGridListModule} from '@angular/material/grid-list';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { PasswordChangeComponent } from './password-change/password-change.component';
import { PasswordChangeEmailDialogComponent } from './password-change-email-dialog/password-change-email-dialog.component';
import {MatCheckboxModule} from '@angular/material/checkbox';

export function getToken()
{
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [																		
    AppComponent,
    NavComponent,
    SideBarComponent,
    RecruitmentCoverletterComponent,
    RecruitmentAdminnotesComponent,
    HomeComponent,
    SearchBarComponent,
    LoginPageComponent,
    CalendarComponent,
    PostsComponent,
    TimePipe,
    RequestsComponent,
    CreateDepartmentComponent,
    ShiftsComponent,
    ProfileViewerComponent,
    RegisterComponent,
    DepartmentsComponent,
    CompanyComponent,
    DocumentsComponent,
    CvComponent,
    RecruitmentComponent,
    TasksComponent,
    CreateShiftComponent,
    MeetingsComponent,
    MeetingsTableComponent,
      CreateTaskComponent,
      CreateMeetingComponent,
      ChangePasswordComponent,
      PasswordChangeComponent,
      PasswordChangeEmailDialogComponent
   ],
  imports: [
    BrowserModule,
    MatTooltipModule,
    AppRoutingModule,
    MatSelectModule,
    ReactiveFormsModule,
    ClipboardModule,
    MatSortModule,
    MatPaginatorModule,
    MatGridListModule,
    MatTabsModule,
    MatCheckboxModule,
    MatDialogModule,
    MatDatepickerModule,
    MatProgressSpinnerModule,
    MatBadgeModule,
    NgbModule,
    MatCardModule,
    MatRippleModule,
    MatTableModule,
    MatDividerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatExpansionModule,
    MatIconModule,
    MatRadioModule,
    MatInputModule,
    LayoutModule,
    MatListModule,
    HttpClientModule,
    TimepickerModule.forRoot(),
    CommonModule,
    BrowserAnimationsModule,
    FormsModule,
    MatTooltipModule,
    TabsModule.forRoot(),
    TypeaheadModule.forRoot(),
    BsDropdownModule.forRoot(),
    RouterModule.forRoot(appRoutes),    
    JwtModule.forRoot({
      config: {
        tokenGetter: getToken,
        allowedDomains: ['localhost:5001'],
        disallowedRoutes: ['localhost:5001/api/auth']
      }})
  ],
  providers: [DatePipe,MatNativeDateModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
