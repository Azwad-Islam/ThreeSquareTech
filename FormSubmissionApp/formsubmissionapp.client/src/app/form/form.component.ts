import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'] // ✅ Ensure it's styleUrls, not styleUrl
})
export class FormComponent {
  formData = { name: '', email: '' };

  constructor(private http: HttpClient) {
    console.log('🚀 FormComponent loaded');
  }

  onSubmit() {
    this.http.post('/api/formdata', this.formData).subscribe({
      next: (res) => console.log('Submitted:', res),
      error: (err) => console.error('Error:', err)
    });
  }
}

