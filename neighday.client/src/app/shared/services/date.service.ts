import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DateService {
  isToday(date: Date) {
    const today = new Date();
    return date.getDate() >= today.getDate();
  }
}
