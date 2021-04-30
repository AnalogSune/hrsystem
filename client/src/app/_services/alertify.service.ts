import * as alertify from 'alertifyjs';

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }

  confirm(message: string, then: () => any) {
    alertify.confirm(message, (e: any) => {
      then();
    });
  }

  success(message: string) {
    alertify.success(message);
  }

  error(message: string, response?: any) {
    if (response && response.error)
      alertify.error(response.error)
    else
      alertify.error(message)
  }

  warning(message: string) {
    alertify.warning(message);
  }

  message(message: string) {
    alertify.message(message);
  }

}
