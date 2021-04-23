import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'time'
})
export class TimePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    const before: Date = new Date(value);
    const now: Date = new Date();
    const seconds = Math.round(Math.abs(now.getTime() - before.getTime())) / 1000;
    if (seconds < 45) 
    {
      return 'Less than a minute ago';
    } 
    else if (seconds < 90) 
    {
      return 'A minute ago';
    } 
    else if (seconds < 3600) 
    {
      return Math.round(seconds / 60) + ' minutes ago';
    } 
    else if (seconds < 86400) 
    {
      return Math.round(seconds / 3600) + ' hours ago';
    } 
    else if (seconds < 86400 * 7) 
    {
      return (Math.round(seconds / 86400).toString()) + ' days ago';
    }
    else if (seconds < 86400 * 30) 
    {
      return (Math.round(seconds / (86400 * 7)).toString()) + ' weeks ago';
    } 
    else if (seconds < 86400 * 365) 
    {
      return (Math.round(seconds / (86400 * 30)).toString()) + ' months ago';
    }
    else 
    {
      return (Math.round(seconds / (86400 * 365)).toString()) + ' years ago';
    }
  }

}
