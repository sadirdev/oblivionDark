using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaungeSystem : MonoBehaviour
{
    public static string RewardExp(int exp)
    {
        lng lang = SS.sv.Lang;
        if (lang == lng.rus) return $"Получено : {exp} единиц опыта.";
        else if (lang == lng.eng) return $"Received: {exp} units of experience.";
        else if (lang == lng.por) return $"Recebido: {exp} pontos de experiência.";
        return "";
    }

    public static string RewardCoins(int coins)
    {
        lng lang = SS.sv.Lang;
        if (lang == lng.rus) return $"Получено : {coins} монет.";
        else if (lang == lng.eng) return $"Received : {coins} coins.";
        else if (lang == lng.por) return $"Recebido: {coins} moedas.";
        return "";
    }

    public static string RewardItem(string nameItem)
    {
        lng lang = SS.sv.Lang;
        if (lang == lng.rus) return $"Получен предмет: {Inventory.NameLang(nameItem)} 1шт.";
        else if (lang == lng.eng) return $"Item received: {Inventory.NameLang(nameItem)} 1 pc.";
        else if (lang == lng.por) return $"Item recebido: {Inventory.NameLang(nameItem)} 1 pc.";
        return "";
    }


    public static string NameLoc()
    {
        lng lang = SS.sv.Lang;

        switch (SS.sv.CrntLoc)
        {
            case Location.BarLoc:
                if (lang == lng.rus) return "Район Фелине";
                else if (lang == lng.eng) return "Feline District";
                else if (lang == lng.por) return "Área Felina";
                break;
            case Location.Bar:
                if (lang == lng.rus) return "Бар «Большая глотка»";
                else if (lang == lng.eng) return "The Big Gulp Bar";
                else if (lang == lng.por) return "Bar «Garganta Grande»";
                break;
            case Location.Cave:
                if (lang == lng.rus) return "Паучий Грот";
                else if (lang == lng.eng) return "Spider Grotto";
                else if (lang == lng.por) return "Gruta das Aranhas";
                break;
            case Location.Kagiyar:
                if (lang == lng.rus) return "Деревня Кагияр";
                else if (lang == lng.eng) return "Kagiyar Village";
                else if (lang == lng.por) return "Vila de Kagiyar";
                break;
            case Location.CityLaboratory:
                if (lang == lng.rus) return "Город B";
                else if (lang == lng.eng) return "City B";
                else if (lang == lng.por) return "Cidade B";
                break;
            case Location.Laboratory:
                if (lang == lng.rus) return "Научная лаборатория";
                else if (lang == lng.eng) return "Scientific Laboratory";
                else if (lang == lng.por) return "Laboratório Científico";
                break;
            case Location.CityZ:
                if (lang == lng.rus) return "Город Z";
                else if (lang == lng.eng) return "City Z";
                else if (lang == lng.por) return "Cidade Z";
                break;
            case Location.Vigiriya:
                if (lang == lng.rus) return "Район Вигирия";
                else if (lang == lng.eng) return "Vigiriya District";
                else if (lang == lng.por) return "Area de Vigiria";
                break;
            case Location.SuperMarket:
                if (lang == lng.rus) return "Супермаркет";
                else if (lang == lng.eng) return "Supermarket";
                else if (lang == lng.por) return "Supermercado";
                break;
            case Location.HomeKitchen:
                if (lang == lng.rus) return "Кухня";
                else if (lang == lng.eng) return "Kitchen";
                else if (lang == lng.por) return "Cozinha";
                break;
            case Location.HomeBedroom:
                if (lang == lng.rus) return "Спальня";
                else if (lang == lng.eng) return "Bedroom";
                else if (lang == lng.por) return "Quarto";
                break;
            case Location.HomeHallway:
                if (lang == lng.rus) return "Прихожая";
                else if (lang == lng.eng) return "Hallway";
                else if (lang == lng.por) return "Antecâmara";
                break;
            case Location.Dump:
                if (lang == lng.rus) return "Мёртвый город";
                else if (lang == lng.eng) return "Dead City";
                else if (lang == lng.por) return "Cidade morta";
                break;
            case Location.Etale:
                if (lang == lng.rus) return "Город Етале";
                else if (lang == lng.eng) return "City of Etale";
                else if (lang == lng.por) return "Cidade de Etale";
                break;
            case Location.HighwayA:
                if (lang == lng.rus) return "Магистраль А";
                else if (lang == lng.eng) return "Highway A";
                else if (lang == lng.por) return "Rodovia A";
                break;
            case Location.HighwayB:
                if (lang == lng.rus) return "Магистраль В";
                else if (lang == lng.eng) return "Highway B";
                else if (lang == lng.por) return "Rodovia B";
                break;
            case Location.North:
                if (lang == lng.rus) return "Замок Веков";
                else if (lang == lng.eng) return "Castle of the Ages";
                else if (lang == lng.por) return "Castelo das Eras";
                break;
            case Location.Portal:
                if (lang == lng.rus) return "Пустырь Мертвого Часа";
                else if (lang == lng.eng) return "Wasteland of the Dead Hour";
                else if (lang == lng.por) return "Deserto da Hora Morta";
                break;
            case Location.PradatoryPath:
                if (lang == lng.rus) return "Хищная тропа";
                else if (lang == lng.eng) return "Predatory Path";
                else if (lang == lng.por) return "Estrada Predatória";
                break;
            case Location.PredatoryHome:
                if (lang == lng.rus) return "Заброшенный дом";
                else if (lang == lng.eng) return "Abandoned house";
                else if (lang == lng.por) return "Casa abandonada";
                break;
            case Location.Shinava:
                if (lang == lng.rus) return "Окраина Шинава";
                else if (lang == lng.eng) return "Outskirts of Shinawa";
                else if (lang == lng.por) return "Arredores de Shinava";
                break;
            case Location.Waterfall:
                if (lang == lng.rus) return "Водопад Келли";
                else if (lang == lng.eng) return "Waterfall Kelly";
                else if (lang == lng.por) return "Cachoeira Kelly";
                break;




        }
        return "";
    }
    public static string NeedCharacterLvl(int lvl)
    {
        lng lang = SS.sv.Lang;
        if (lang == lng.rus) return $"Для продолжения синхронизации вы должны быть героем {Clicker.ClassReturn(lvl)} класса.";
        else if (lang == lng.eng) return $"To continue syncing, you must be an {Clicker.ClassReturn(lvl)} class hero.";
        else if (lang == lng.por) return $"Você deve ser um herói da classe {Clicker.ClassReturn(lvl)} para continuar sincronizando.";
        return "";
    }
    public static string Word(string word)
    {
        lng lang = SS.sv.Lang;

        switch (word)
        {
            case "Coins":
                if (lang == lng.rus) return "Монет";
                else if (lang == lng.eng) return "Coins";
                else if (lang == lng.por) return "Moedas";
                break;
            case "NewBlock":
                if (lang == lng.rus) return "Новый блок синхронизации «Kepler 360» открыт.";
                else if (lang == lng.eng) return "The new «Kepler 360» synchronization unit is open.";
                else if (lang == lng.por) return "O novo bloco de sincronização do «Kepler 360» está aberto.";
                break;
            case "NoEnergy":
                if (lang == lng.rus) return "Не получилось! У Вас закончилась энергия.";
                else if (lang == lng.eng) return "It didn't work out! You've run out of energy.";
                else if (lang == lng.por) return "Não funcionou! Você ficou sem energia.";
                break;
            case "NewEnemy":
                if (lang == lng.rus) return "Доступен новый соперник в кулачных боях.";
                else if (lang == lng.eng) return "A new opponent in fist fights is available.";
                else if (lang == lng.por) return "Um novo inimigo está disponível para batalhas.";
                break;
            case "NowWay":
                if (lang == lng.rus) return "Очень темное место, лучше туда не спускаться.";
                else if (lang == lng.eng) return "It's a very dark place, it's better not to go down there.";
                else if (lang == lng.por) return "Lugar muito escuro, é melhor não descer lá.";
                break;
            case "KillSpider":
                if (lang == lng.rus) return "Вы срaзили Королеву Пауков. Отправляйтесь к правителю поселения.";
                else if (lang == lng.eng) return "You have defeated the Spider Queen. Go to the ruler of the settlement.";
                else if (lang == lng.por) return "Você derrotou a Rainha Aranha. Vá até o governador do assentamento.";
                break;
            case "CloseDoor":
                if (lang == lng.rus) return "Дверь заперта. Сайтама сейчас в супермаркете.";
                else if (lang == lng.eng) return "The door is locked. Saitama is at the supermarket now.";
                else if (lang == lng.por) return "A porta está trancada. Saitama está no supermercado agora.";
                break;
            case "ClearShmot":
                if (lang == lng.rus) return "Грязные вещи постираны. Получен предмет: чистый плащ Сайтамы 1шт.";
                else if (lang == lng.eng) return "Dirty things are washed. Item received: Saitama's clean cloak.";
                else if (lang == lng.por) return "As coisas sujas são lavadas. Item obtido: Manto limpo de Saitama.";
                break;
            case "NoDirtyShmot":
                if (lang == lng.rus) return "У вас нет грязных вещей.";
                else if (lang == lng.eng) return "You don't have dirty things.";
                else if (lang == lng.por) return "Você não tem coisas sujas.";
                break;
            case "NoPowder":
                if (lang == lng.rus) return "У Вас нет стирального порошка.";
                else if (lang == lng.eng) return "You don't have laundry detergent.";
                else if (lang == lng.por) return "Você não tem sabão em pó.";
                break;
            case "KeplerNeed3lvl":
                if (lang == lng.rus) return "Для продолжения синхронизации Kepler 360 должен быть 3 уровня.";
                else if (lang == lng.eng) return "To continue syncing, Kepler 360 must be level 3.";
                else if (lang == lng.por) return "Para continuar a sincronização, Kepler 360 deve estar no nível 3.";
                break;
            case "KeplerNeed6lvl":
                if (lang == lng.rus) return "Для продолжения синхронизации Kepler 360 должен быть 6 уровня.";
                else if (lang == lng.eng) return "To continue syncing, Kepler 360 must be level 6.";
                else if (lang == lng.por) return "Para continuar a sincronização, Kepler 360 deve estar no nível 6.";
                break;
            case "NeedNoodles":
                if (lang == lng.rus) return "Для приготовления рамена нужна лапша. Купите ее в супермаркете.";
                else if (lang == lng.eng) return "Noodles are needed to make ramen. Buy it at the supermarket.";
                else if (lang == lng.por) return "Você precisa de macarrão para fazer ramen. Você precisa comprá-lo no supermercado.";
                break;
            case "NeedHands":
                if (lang == lng.rus) return "Для уборки квартиры Геносу нужены Руки домашних забот.";
                else if (lang == lng.eng) return "Genos needs «Hands of household chores» to clean the apartment.";
                else if (lang == lng.por) return "Para limpar o apartamento, Genos precisa de «Mãos para tarefas domésticas»";
                break;
            case "ScreenNoPower":
                if (lang == lng.rus) return "Монитор не включается, видимо, сломан.";
                else if (lang == lng.eng) return "The monitor does not turn on, apparently broken.";
                else if (lang == lng.por) return "O monitor não liga, aparentemente quebrado.";
                break;
            case "RepairMonitor":
                if (lang == lng.rus) return "Для ремонта монитора нужен текстолит, кремний и 3 полупроводника.";
                else if (lang == lng.eng) return "To repair the monitor, you need a textolite, silicon and 3 semiconductors.";
                else if (lang == lng.por) return "Para reparar o monitor, você precisará de textolite, silício e 3 semicondutores.";
                break;
            case "BuildingHeroClose":
                if (lang == lng.rus) return "Здание ассоциации героев закрыто.";
                else if (lang == lng.eng) return "The building of the Association of Heroes is closed.";
                else if (lang == lng.por) return "O prédio da Hero Association está fechado.";
                break;
            case "NotKatana":
                if (lang == lng.rus) return "У Вас нет катаны.";
                else if (lang == lng.eng) return "You don't have a katana.";
                else if (lang == lng.por) return "Você não tem uma katana.";
                break;
            case "GateClosed":
                if (lang == lng.rus) return "Ворота в замок закрыты. Дальше идти нет смысла.";
                else if (lang == lng.eng) return "The gates to the castle are closed. There's no point in going any further.";
                else if (lang == lng.por) return "Os portões do castelo estão fechados. Não adianta ir mais longe.";
                break;
            case "Rafureshidon":
                if (lang == lng.rus) return "Открыв с помощью ключа замок и шагнув в темноту оказавшегося за дверью помещения, сделав еще пару шагов вы поняли, что находитесь среди огромных зарослей. Ветки дрожат, быстро передвигаясь по ним, к вам приближается зловещий Помойкоцвет.";
                else if (lang == lng.eng) return "Having opened the lock with a key and stepped into the darkness of the room behind the door, after taking a couple more steps, you realized that you were among huge thickets. The branches are shaking, moving quickly along them, an ominous Rafureshidon is approaching you.";
                else if (lang == lng.por) return "Abrindo a fechadura com a chave e entrando na escuridão do quarto do lado de fora da porta, dando mais alguns passos, você percebeu que estava entre enormes moitas. Os galhos estão tremendo, movendo-se rapidamente ao longo deles, o sinistro Rafureshidonestá se aproximando de você.";
                break;
            case "RafureshidonFinish":
                if (lang == lng.rus) return "Расправившись с Помойкоцветом и внимательно обыскав подвал, в углу под кучей мусора вы обнаружили электронный пропуск Зункоти в лабораторию и немного монет. К сожалению, не обошлось и без потерь - во время сражения вы обронили Ключ от дома и он упал в болото.";
                else if (lang == lng.eng) return "Having dealt with Rafureshidon and carefully searched the basement, in the corner under a pile of garbage you found an electronic Zuncoti pass to the laboratory and some coins. Unfortunately, it was not without losses - during the battle you dropped the Key to the house and it fell into the swamp.";
                else if (lang == lng.por) return "Depois de lidar com Rafureshidon ​​e vasculhar cuidadosamente o porão, no canto sob uma pilha de lixo, você encontrou o passe eletrônico de Zunkoti para o laboratório e algumas moedas. Infelizmente, houve algumas perdas - durante a batalha você deixou cair a Chave da Casa e ela caiu no pântano.";
                break;
            case "DoorClose":
                if (lang == lng.rus) return "Все попытки открыть дверь оказались безуспешны. Дом хоть и ветхий, но дверь крепкая.";
                else if (lang == lng.eng) return "All attempts to open the door were unsuccessful. The house is dilapidated, but the door is strong.";
                else if (lang == lng.por) return "Todas as tentativas de abrir a porta foram infrutíferas. A casa é velha, mas a porta é forte.";
                break;
            case "СлышимВопль":
                if (lang == lng.rus) return "Попытавшись открыть дверь, вы услышали сзади шелест листьев и омерзительный вопль...";
                else if (lang == lng.eng) return "When you tried to open the door, you heard the rustle of leaves and a disgusting scream from behind...";
                else if (lang == lng.por) return "Quando você tentou abrir a porta, ouviu o farfalhar de folhas atrás de você e um grito nojento...";
                break;
            case "ЗапертаДверь":
                if (lang == lng.rus) return "Дом давно уже заброшен, но дверь в нём заперта.";
                else if (lang == lng.eng) return "The house has been abandoned for a long time, but the door is locked in it.";
                else if (lang == lng.por) return "A casa está abandonada há muito tempo, mas a porta está trancada.";
                break;
            case "РепутацияСамурая":
                if (lang == lng.rus) return "Репутация Атомного Самурая увеличена.";
                else if (lang == lng.eng) return "The reputation of the Atomic Samurai has been increased.";
                else if (lang == lng.por) return "A reputação do Samurai Atômico aumentou.";
                break;
            case "СундукЗакрыт":
                if (lang == lng.rus) return "Сундук не получилось открыть.";
                else if (lang == lng.eng) return "It didn't work out! The chest is closed.";
                else if (lang == lng.por) return "Não funcionou!";
                break;
            case "Забрать":
                if (lang == lng.rus) return "Забрать";
                else if (lang == lng.eng) return "Take";
                else if (lang == lng.por) return "Leva";
                break;
            case "НетУдочки":
                if (lang == lng.rus) return "У вас нет удочки для ловли рыбы.";
                else if (lang == lng.eng) return "You don't have a fishing rod to catch fish.";
                else if (lang == lng.por) return "Você não tem uma vara de pesca para pescar.";
                break;
            case "НуженИнтернет":
                if (lang == lng.rus) return "Необходимо подключение к Интернету.";
                else if (lang == lng.eng) return "An internet connection is required.";
                else if (lang == lng.por) return "É necessária uma conexão com a internet.";
                break;
            case "ГородДалеко":
                if (lang == lng.rus) return "Чтобы добраться в данную область, нужен перевозчик.";
                else if (lang == lng.eng) return "To get to this area, you need a carrier.";
                else if (lang == lng.por) return "Para chegar a esta área, você precisa de uma transportadora.";
                break;



                
        }
       
        return "";
    }
}
