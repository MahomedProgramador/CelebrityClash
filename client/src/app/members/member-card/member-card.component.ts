import { Component, computed, inject, input } from '@angular/core';
import { Member } from '../../_models/members';
import { RouterLink } from '@angular/router';
import { DislikesService } from '../../_services/dislikes.service';

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css'
})
export class MemberCardComponent {
  private dislikeService = inject(DislikesService)
  member = input.required<Member>();
  hasDisliked = computed(() => this.dislikeService.dislikesIds().includes(this.member().id))

  toggleDislike(){
    this.dislikeService.toggleDislike(this.member().id).subscribe({
      next: () => {
        if(this.hasDisliked()){
          this.dislikeService.dislikesIds.update(ids => ids.filter(x => x !== this.member().id))
        } else{
          this.dislikeService.dislikesIds.update(ids => [...ids, this.member().id])
        }
      }
    })
  }
}
