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
import {ClipboardModule} from '@angular/cdk/clipboard';
import {TimepickerModule} from 'ngx-bootstrap/timepicker';
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

export function getToken()
{
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [													
    AppComponent,
    NavComponent,
    SideBarComponent,
    HomeComponent,
    SearchBarComponent,
    LoginPageComponent,
    CalendarComponent,
    PostsComponent,
    TimePipe,
    RequestsComponent,
    ShiftsComponent,
    ProfileViewerComponent,
    RegisterComponent,
    DepartmentsComponent,
    CompanyComponent,
    DocumentsComponent,
    CvComponent,
    RecruitmentComponent,
    TasksComponent,
    MeetingsComponent
   ],
  imports: [
    BrowserModule,
    MatTooltipModule,
    AppRoutingModule,
    MatSelectModule,
    ClipboardModule,
    NgbModule,
    LayoutModule,
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
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
