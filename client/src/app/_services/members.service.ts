import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/members';
import { of, tap } from 'rxjs';
import { Photo } from '../_models/photos';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  members = signal<Member[]>([]);

  getMembers() {
    if (this.members().length > 0) return;
    return this.http.get<Member[]>(this.baseUrl + 'users').subscribe({
      next: members => this.members.set(members)      
    })
  }
  
  getMember(username: string) {
    const member = this.members().find(m => m.username === username);
    if (member !== undefined) return of(member);

    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }
  
  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      tap(() => {
          this.members.update(members => members.map(m => m.username === member.username ? member : m))
      })
    );
  }

  setMainPhoto(photo: Photo) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photo.id, {}).pipe(
      tap(() => {
          this.members.update(members => members.map(m => {
            if (m.photos.includes(photo)){
              m.photoUrl = photo.url;
            }
            return m;
          }))
      })
    );
  }

  deletePhoto(photo: Photo) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photo.id).pipe(
      tap(() => {
          this.members.update(members => members.map(m => {
            if (m.photos.includes(photo)) {
              m.photos = m.photos.filter(x => x.id !== photo.id);
            }           
            return m;
          }))
      })
    )
  }
}
