import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatCardModule } from "@angular/material/card";
import { AppButtonModule } from "@root/components/button/button.module";
import { CardUiComponent } from "./card-ui/card-ui.component";
import { ElipsisPipe } from "@root/components/elipsis.pipe";

@NgModule({
	declarations: [CardUiComponent, ElipsisPipe],
	imports: [CommonModule, MatCardModule, AppButtonModule],
	exports: [CardUiComponent],
})
export class CardUiModule {}
