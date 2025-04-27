import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-display',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './display.component.html'
})
export class DisplayComponent implements OnInit {
  submissions: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get<any[]>('https://localhost:7153/api/formdata').subscribe({
      next: (data) => {
        this.submissions = data;
        console.log('Loaded submissions:', data);
      },
      error: (err) => {
        console.error('Error fetching submissions:', err);
      }
    });
  }
}
