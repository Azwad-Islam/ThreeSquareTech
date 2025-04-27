import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './form.component.html',
})
export class FormComponent {
  formData = { name: '', email: '' };
  code: string | null = null;

  constructor(private http: HttpClient, private route: ActivatedRoute) {
    this.route.queryParamMap.subscribe(params => {
      this.code = params.get('code');
      console.log('URL code:', this.code);
    });
  }

  onSubmit() {
    const payload = {
      ...this.formData,
      code: this.code // Add the code along with the form data
    };

    this.http.post('https://localhost:7153/api/formdata', payload).subscribe({
      next: (res) => {
        console.log('Submitted successfully:', res);
        alert('Form submitted!');
      },
      error: (err) => {
        console.error('Error submitting form:', err);
        alert('Error submitting.');
      }
    });
  }
}
