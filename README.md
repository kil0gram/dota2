# dota2
dota statistics tool

this tool takes steam json response and converts it into .net object, still a work in progress but a few of the key features have been implemented
https://github.com/kil0gram/dota2/new/initmaster?readme=1#fullscreen

            //Get list of latest heroes with names parsed/cleaned
            List<Heroes.Hero> heros = Heroes.GetHeroes();
            
            //get latest 100 matches with brief details (no hero items/abilities/build info)
            List<Match> latest100matches = MatchHistory.GetMatchHistory(heros);

            //Get match details for match id 1277955116
            MatchDetails.MatchDetailsResult matchdetails = MatchDetails.GetMatchDetail(1277955116, heros);
