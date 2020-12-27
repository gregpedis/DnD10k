import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app-component/app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ApiVersionInterceptor } from './interceptors/api-version-interceptor';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })],
  bootstrap: [AppComponent],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: ApiVersionInterceptor, multi: true }],
})
export class AppModule { }
