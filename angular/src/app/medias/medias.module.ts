import { NgModule } from "@angular/core";
import { MediasListComponent } from "./list/medias-list.component";
import { CommonModule } from "@angular/common";
import { MediasRoutingModule } from "./medias-routing.module";
import { AppListModule } from "@root/components/list/list.module";
import { MediasRoutingComponent } from "./medias-routing.component";

@NgModule({
	declarations: [MediasRoutingComponent, MediasListComponent],
	imports: [CommonModule, MediasRoutingModule, AppListModule],
	exports: [MediasListComponent],
})
export class MediasModule {}
