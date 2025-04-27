import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-display',
  templateUrl: './display.component.html',
  styleUrls: ['./display.component.css'] // ✅ Correct this if you wrote styleUrl
})
export class DisplayComponent {
  data: any[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<any[]>('/api/formdata').subscribe(res => {
      this.data = res;
    });
  }
}
