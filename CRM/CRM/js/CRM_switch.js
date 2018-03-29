function switch_ove(){			
			var objths = vm.$data.item;
			var A_durationneed = objths.A_durationneed;
			var A_acceptanceneed = objths.A_acceptanceneed;
			var A_constructneed = objths.A_constructneed;
			var A_defaultmakeup = objths.A_defaultmakeup;
			var A_aptitudes = objths.A_aptitudes;
			var A_projectscale = objths.A_projectscale;
			var A_fundsbackfront = objths.A_fundsbackfront;
			var A_budget = objths.A_budget;
			var A_kickbackneed = objths.A_kickbackneed;
			var A_projectdrag = objths.A_projectdrag;
			var A_compete = objths.A_compete;
			var A_consumerFocus = objths.A_consumerFocus;
			var A_consumerkey = objths.A_consumerkey;
			var A_competitiveness = objths.A_competitiveness;
			var A_conclusion = objths.A_conclusion;
			switch(A_durationneed){
				case 0:{
					objths.A_durationneed = '苛刻';					
				};break;
				case 3:{
					objths.A_durationneed = '严格';										
				};break;
				case 5:{
					objths.A_durationneed = '宽松';
				};break;
				default:break;
			}
			switch(A_acceptanceneed){
				case 0:{
					objths.A_acceptanceneed = '苛刻';
				};break;
				case 3:{
					objths.A_acceptanceneed = '严格';
				};break;
				case 5:{
					objths.A_acceptanceneed = '宽松';
				};break;
				default:break;
			}
			switch(A_constructneed){
				case 0:{
					objths.A_constructneed = '苛刻';
				};break;
				case 3:{
					objths.A_constructneed = '严格';					
				};break;
				case 5:{
					objths.A_constructneed = '宽松';
				};break;
				default:break;
			}
			switch(A_defaultmakeup){
				case 0:{
					objths.A_defaultmakeup = '苛刻';
				};break;
				case 1:{
					objths.A_defaultmakeup = '无';
				};break;
				case 3:{
					objths.A_defaultmakeup = '严格';
					
				};break;
				case 5:{
					objths.A_defaultmakeup = '宽松';
				};break;
				default:break;
			}
			switch(A_aptitudes){
				case 0:{
					objths.A_aptitudes = '没有实力';
					
				};break;
				case 10:{
					objths.A_aptitudes = '一般';
					
				};break;
				case 20:{
					objths.A_aptitudes = '有实力';				
				};break;
				default:break;
			}
			switch(A_projectscale){
				case 5:{
					objths.A_projectscale = '小型';
				};break;
				case 10:{
					objths.A_projectscale = '中型';
				};break;
				case 15:{
					objths.A_projectscale = '大型';
					
				};break;
				case 20:{
					objths.A_projectscale = '超大型';
				};break;
				default:break;
			}			
			switch(A_fundsbackfront){
				case 0:{
					objths.A_fundsbackfront = '没有实力';
					
				};break;
				case 10:{
					objths.A_fundsbackfront = '账期一个月内';
					
				};break;
				case 20:{
					objths.A_fundsbackfront = '快';				
				};break;
				default:break;
			}
			
			switch(A_budget){
				case 5:{
					objths.A_budget = '1万';
				};break;
				case 10:{
					objths.A_budget = '1万-5万';
				};break;
				case 15:{
					objths.A_budget = '5万-10万';
					
				};break;
				case 20:{
					objths.A_budget = '>10万';
				};break;
				default:break;
			}
			
			switch(A_kickbackneed){
				case 0:{
					objths.A_kickbackneed = '没有';
					
				};break;
				case 5:{
					objths.A_kickbackneed = '有';
					
				};break;
				default:break;
			}
			
			switch(A_projectdrag){
				case 10:{
					objths.A_projectdrag = '没有';
					
				};break;
				case 2:{
					objths.A_projectdrag = '有';
					
				};break;
				default:break;
			}
			
			switch(A_compete){
				case 5:{
					objths.A_compete = '不清楚';
				};break;
				case 10:{
					objths.A_compete = '强';
				};break;
				case 15:{
					objths.A_compete = '不强';
					
				};break;
				case 20:{
					objths.A_compete = '无';
				};break;
				default:break;
			}			
			switch(A_consumerFocus){
				case 5:{
					objths.A_consumerFocus = '价格';
				};break;
				case 9:{
					objths.A_consumerFocus = '品牌';
					
				};break;
				case 13:{
					objths.A_consumerFocus = '质量';
					
				};break;
				case 15:{
					objths.A_consumerFocus = '性价比';
				};break;
				default:break;
			}
			
			switch(A_consumerkey){
				case 0:{
					objths.A_consumerkey = '没有';
				};break;
				case 5:{
					objths.A_consumerkey = '一点';
				};break;
				case 15:{
					objths.A_consumerkey = '可以';
					
				};break;
				case 25:{
					objths.A_consumerkey = '搞定';
				};break;
				default:break;
			}
			
			switch(A_competitiveness){
				case 5:{
					objths.A_competitiveness = '弱';
				};break;
				case 10:{
					objths.A_competitiveness = '一般';
				};break;
				case 25:{
					objths.A_competitiveness = '强';
					
				};break;
				default:break;
			}
			
			switch(A_conclusion){
				case 0:{
					objths.A_conclusion = '放弃';
				};break;
				case 1:{
					objths.A_conclusion = '修改筛选结果';
				};break;
				case 2:{
					objths.A_conclusion = '普通商机';
					
				};break;
				case 3:{
					objths.A_conclusion = '重点商机';
				};break;
				case 4:{
					objths.A_conclusion = '鸭子商机';
				};break;
				default:break;
			}
		}
//商品详情里的
function switch_oves(){			
			var objths = vm.$data.as;
			var A_durationneed = objths.A_durationneed;
			var A_acceptanceneed = objths.A_acceptanceneed;
			var A_constructneed = objths.A_constructneed;
			var A_defaultmakeup = objths.A_defaultmakeup;
			var A_aptitudes = objths.A_aptitudes;
			var A_projectscale = objths.A_projectscale;
			var A_fundsbackfront = objths.A_fundsbackfront;
			var A_budget = objths.A_budget;
			var A_kickbackneed = objths.A_kickbackneed;
			var A_projectdrag = objths.A_projectdrag;
			var A_compete = objths.A_compete;
			var A_consumerFocus = objths.A_consumerFocus;
			var A_consumerkey = objths.A_consumerkey;
			var A_competitiveness = objths.A_competitiveness;
			var A_conclusion = objths.A_conclusion;
			switch(A_durationneed){
				case 0:{
					objths.A_durationneed = '苛刻';					
				};break;
				case 3:{
					objths.A_durationneed = '严格';										
				};break;
				case 5:{
					objths.A_durationneed = '宽松';
				};break;
				default:break;
			}
			switch(A_acceptanceneed){
				case 0:{
					objths.A_acceptanceneed = '苛刻';
				};break;
				case 3:{
					objths.A_acceptanceneed = '严格';
				};break;
				case 5:{
					objths.A_acceptanceneed = '宽松';
				};break;
				default:break;
			}
			switch(A_constructneed){
				case 0:{
					objths.A_constructneed = '苛刻';
				};break;
				case 3:{
					objths.A_constructneed = '严格';					
				};break;
				case 5:{
					objths.A_constructneed = '宽松';
				};break;
				default:break;
			}
			switch(A_defaultmakeup){
				case 0:{
					objths.A_defaultmakeup = '苛刻';
				};break;
				case 1:{
					objths.A_defaultmakeup = '无';
				};break;
				case 3:{
					objths.A_defaultmakeup = '严格';
					
				};break;
				case 5:{
					objths.A_defaultmakeup = '宽松';
				};break;
				default:break;
			}
			switch(A_aptitudes){
				case 0:{
					objths.A_aptitudes = '没有实力';
					
				};break;
				case 10:{
					objths.A_aptitudes = '一般';
					
				};break;
				case 20:{
					objths.A_aptitudes = '有实力';				
				};break;
				default:break;
			}
			switch(A_projectscale){
				case 5:{
					objths.A_projectscale = '小型';
				};break;
				case 10:{
					objths.A_projectscale = '中型';
				};break;
				case 15:{
					objths.A_projectscale = '大型';
					
				};break;
				case 20:{
					objths.A_projectscale = '超大型';
				};break;
				default:break;
			}			
			switch(A_fundsbackfront){
				case 0:{
					objths.A_fundsbackfront = '没有实力';
					
				};break;
				case 10:{
					objths.A_fundsbackfront = '账期一个月内';
					
				};break;
				case 20:{
					objths.A_fundsbackfront = '快';				
				};break;
				default:break;
			}
			
			switch(A_budget){
				case 5:{
					objths.A_budget = '1万';
				};break;
				case 10:{
					objths.A_budget = '1万-5万';
				};break;
				case 15:{
					objths.A_budget = '5万-10万';
					
				};break;
				case 20:{
					objths.A_budget = '>10万';
				};break;
				default:break;
			}
			
			switch(A_kickbackneed){
				case 0:{
					objths.A_kickbackneed = '没有';
					
				};break;
				case 5:{
					objths.A_kickbackneed = '有';
					
				};break;
				default:break;
			}
			
			switch(A_projectdrag){
				case 10:{
					objths.A_projectdrag = '没有';
					
				};break;
				case 2:{
					objths.A_projectdrag = '有';
					
				};break;
				default:break;
			}
			
			switch(A_compete){
				case 5:{
					objths.A_compete = '不清楚';
				};break;
				case 10:{
					objths.A_compete = '强';
				};break;
				case 15:{
					objths.A_compete = '不强';
					
				};break;
				case 20:{
					objths.A_compete = '无';
				};break;
				default:break;
			}			
			switch(A_consumerFocus){
				case 5:{
					objths.A_consumerFocus = '价格';
				};break;
				case 9:{
					objths.A_consumerFocus = '品牌';
					
				};break;
				case 13:{
					objths.A_consumerFocus = '质量';
					
				};break;
				case 15:{
					objths.A_consumerFocus = '性价比';
				};break;
				default:break;
			}
			
			switch(A_consumerkey){
				case 0:{
					objths.A_consumerkey = '没有';
				};break;
				case 5:{
					objths.A_consumerkey = '一点';
				};break;
				case 15:{
					objths.A_consumerkey = '可以';
					
				};break;
				case 25:{
					objths.A_consumerkey = '搞定';
				};break;
				default:break;
			}
			
			switch(A_competitiveness){
				case 5:{
					objths.A_competitiveness = '弱';
				};break;
				case 10:{
					objths.A_competitiveness = '一般';
				};break;
				case 25:{
					objths.A_competitiveness = '强';
					
				};break;
				default:break;
			}
			
			switch(A_conclusion){
				case 0:{
					objths.A_conclusion = '放弃';
				};break;
				case 1:{
					objths.A_conclusion = '修改筛选结果';
				};break;
				case 2:{
					objths.A_conclusion = '普通商机';
					
				};break;
				case 3:{
					objths.A_conclusion = '重点商机';
				};break;
				case 4:{
					objths.A_conclusion = '鸭子商机';
				};break;
				default:break;
			}
		}