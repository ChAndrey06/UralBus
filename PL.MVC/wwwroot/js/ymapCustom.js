(async () => {
if(document.getElementById('map')){
  let marksApi,
  points
  // await fetch('http://localhost:5058/trips/points?id=c1c9a95e-0cd1-476a-9307-7dcc7e9f3762')
  await fetch('https://uralbus-stage.spaceapp.ru/trips/points?id=fe93b6b4-5f02-46df-9a4d-5f2528e09bd2')
    .then((response) => {
      return response.json();
    })
    .then((data) => {
      for (let i = 0; i < data.length; i++) {
        for (let j = 0; j < Object.values(data[i]).length; j++) {
          if (typeof Object.values(data[i])[i,j] !== 'string') {
            console.log('Неверный тип данных');
            return;
          }
        }
      }
      marksApi = data
      points = marksApi.map(obj => Object.values(obj));
    })
    .catch(error => console.error(error))
  
   
  var myMap;
  var oldCenter;
  var myCollection;
  var arrayBalloons = [];
  await ymaps.ready(function () {
    myCollection = new ymaps.GeoObjectCollection();
    myMap = new ymaps.Map("map", {
        center: [51.545477, 46.014368], 
        zoom: 11,         
        controls: ['zoomControl'],
    });
    if($(window).width() < 1024) {
        myMap.behaviors.disable('scrollZoom');
        myMap.behaviors.disable('drag');
    }
    // РџРµСЂРµР±РёСЂР°РµРј РІ С†РёРєР»Рµ С‚РѕС‡РєРё, РєРѕС‚РѕСЂС‹Рµ РЅР°РґРѕ РґРѕР±Р°РІРёС‚СЊ РЅР° РєР°СЂС‚Сѓ
    
  
    if(marksApi){
      var multiRoute = new ymaps.multiRouter.MultiRoute({
        referencePoints: points,
        viaPoints: [],
        params: {
          results: 1,
        }
      },
       {
        // Р’РЅРµС€РЅРёР№ РІРёРґ РїСѓС‚РµРІС‹С… С‚РѕС‡РµРє.
        // Р—Р°РґР°РµРј СЃРѕР±СЃС‚РІРµРЅРЅСѓСЋ РєР°СЂС‚РёРЅРєСѓ РґР»СЏ РїРѕСЃР»РµРґРЅРµР№ РїСѓС‚РµРІРѕР№ С‚РѕС‡РєРё.
    
        // РџРѕР·РІРѕР»СЏРµС‚ СЃРєСЂС‹С‚СЊ РёРєРѕРЅРєРё РїСѓС‚РµРІС‹С… С‚РѕС‡РµРє РјР°СЂС€СЂСѓС‚Р°.
         wayPointVisible:false,
    
        // Р’РЅРµС€РЅРёР№ РІРёРґ С‚СЂР°РЅР·РёС‚РЅС‹С… С‚РѕС‡РµРє.
    
        // РўСЂР°РЅР·РёС‚РЅС‹Рµ С‚РѕС‡РєРё РјРѕР¶РЅРѕ РїРµСЂРµС‚Р°СЃРєРёРІР°С‚СЊ, РїСЂРё СЌС‚РѕРј
        // РјР°СЂС€СЂСѓС‚ Р±СѓРґРµС‚ РїРµСЂРµСЃС‚СЂР°РёРІР°С‚СЊСЃСЏ.
        // РџРѕР·РІРѕР»СЏРµС‚ СЃРєСЂС‹С‚СЊ РёРєРѕРЅРєРё С‚СЂР°РЅР·РёС‚РЅС‹С… С‚РѕС‡РµРє РјР°СЂС€СЂСѓС‚Р°.
        viaPointVisible:false,
    
        // Р’РЅРµС€РЅРёР№ РІРёРґ С‚РѕС‡РµС‡РЅС‹С… РјР°СЂРєРµСЂРѕРІ РїРѕРґ РїСѓС‚РµРІС‹РјРё С‚РѕС‡РєР°РјРё.
        // РџРѕР·РІРѕР»СЏРµС‚ СЃРєСЂС‹С‚СЊ С‚РѕС‡РµС‡РЅС‹Рµ РјР°СЂРєРµСЂС‹ РїСѓС‚РµРІС‹С… С‚РѕС‡РµРє.
        pinVisible:false,
        wayPointVisible: false,
        // Р’РЅРµС€РЅРёР№ РІРёРґ Р»РёРЅРёРё РјР°СЂС€СЂСѓС‚Р°.
        routeStrokeWidth: 2,
        routeStrokeColor: "#00b0ff",
        routeActiveStrokeWidth: 6,
        routeActiveStrokeColor: "#00b0ff",
    
        // Р’РЅРµС€РЅРёР№ РІРёРґ Р»РёРЅРёРё РїРµС€РµС…РѕРґРЅРѕРіРѕ РјР°СЂС€СЂСѓС‚Р°.
        routeActivePedestrianSegmentStrokeStyle: "solid",
        routeActivePedestrianSegmentStrokeColor: "#1967d2",
    
        // РђРІС‚РѕРјР°С‚РёС‡РµСЃРєРё СѓСЃС‚Р°РЅР°РІР»РёРІР°С‚СЊ РіСЂР°РЅРёС†С‹ РєР°СЂС‚С‹ С‚Р°Рє, С‡С‚РѕР±С‹ РјР°СЂС€СЂСѓС‚ Р±С‹Р» РІРёРґРµРЅ С†РµР»РёРєРѕРј.
        boundsAutoApply: true
    });
    myMap.geoObjects.add(multiRoute);
    }
    // var multiRoute = new ymaps.multiRouter.MultiRoute({
    //   // РћРїРёСЃР°РЅРёРµ РѕРїРѕСЂРЅС‹С… С‚РѕС‡РµРє РјСѓР»СЊС‚РёРјР°СЂС€СЂСѓС‚Р°.
    //   referencePoints: [
    //       [51.538419, 46.034104],
    //       [55.756000, 37.617820],
    //   ],
    //   // РџР°СЂР°РјРµС‚СЂС‹ РјР°СЂС€СЂСѓС‚РёР·Р°С†РёРё.
    //   params: {
    //       // РћРіСЂР°РЅРёС‡РµРЅРёРµ РЅР° РјР°РєСЃРёРјР°Р»СЊРЅРѕРµ РєРѕР»РёС‡РµСЃС‚РІРѕ РјР°СЂС€СЂСѓС‚РѕРІ, РІРѕР·РІСЂР°С‰Р°РµРјРѕРµ РјР°СЂС€СЂСѓС‚РёР·Р°С‚РѕСЂРѕРј.
    //       results: 1
    //   }
    // }, {
    //   // РђРІС‚РѕРјР°С‚РёС‡РµСЃРєРё СѓСЃС‚Р°РЅР°РІР»РёРІР°С‚СЊ РіСЂР°РЅРёС†С‹ РєР°СЂС‚С‹ С‚Р°Рє, С‡С‚РѕР±С‹ РјР°СЂС€СЂСѓС‚ Р±С‹Р» РІРёРґРµРЅ С†РµР»РёРєРѕРј.
    //   boundsAutoApply: true
    // });
    //myMap.geoObjects.add(multiRoute);
    // Р”РѕР±Р°РІР»СЏРµРј С‚РѕС‡РєРё РЅР° РєР°СЂС‚Сѓ
  
    // Р’С‹С‡РёСЃР»СЏРµРј РЅРµРѕР±С…РѕРґРёРјС‹Рµ РєРѕРѕСЂРґРёРЅР°С‚С‹ РєСЂР°С‘РІ РєР°СЂС‚С‹ Рё
    //  СѓСЃС‚Р°РЅР°РІР»РёРІР°РµРј РёС… РґР»СЏ РЅР°С€РµРіРѕ СЌРєР·РµРјРїР»СЏСЂР° РєР°СЂС‚С‹  
    //myMap.setBounds( myCollection.getBounds() );
  
  });
  function setCenter(x, y) {
    ymaps.ready(function () {
        myMap.setCenter([x, y], 13, {
            checkZoomRange: false
        });
    });
  };
  
  function setCenterPos(x, y, i) {
    ymaps.ready(function () {
  
        var myPlacemark = new ymaps.Placemark([
          x,y
  
        ], {
            //hintContent: points[i][0],
            balloonContent: points[i]
        }, {
          // РќРµРѕР±С…РѕРґРёРјРѕ СѓРєР°Р·Р°С‚СЊ РґР°РЅРЅС‹Р№ С‚РёРї РјР°РєРµС‚Р°.
          iconLayout: 'default#image',
          // РЎРІРѕС‘ РёР·РѕР±СЂР°Р¶РµРЅРёРµ РёРєРѕРЅРєРё РјРµС‚РєРё.
          iconImageHref: 'img/picture/Vector (15).svg',
          // Р Р°Р·РјРµСЂС‹ РјРµС‚РєРё.
          iconImageSize: [30, 30],
          // РЎРјРµС‰РµРЅРёРµ Р»РµРІРѕРіРѕ РІРµСЂС…РЅРµРіРѕ СѓРіР»Р° РёРєРѕРЅРєРё РѕС‚РЅРѕСЃРёС‚РµР»СЊРЅРѕ
          // РµС‘ "РЅРѕР¶РєРё" (С‚РѕС‡РєРё РїСЂРёРІСЏР·РєРё).
          iconImageOffset: [-20, -20],
          balloonLayout: "default#imageWithContent",
  
        });
        // РќРµ Р·Р°Р±С‹РІР°РµРј РґРѕР±Р°РІРёС‚СЊ С‚РѕС‡РєСѓ РІ РєРѕР»Р»РµРєС†РёСЋ -
      //  РІРїРѕСЃР»РµРґСЃС‚РІРёРё РјС‹ РґРѕР±Р°РІРёРј РІСЃСЋ РєРѕР»Р»РµРєС†РёСЋ РЅР° РєР°СЂС‚Сѓ
  
        //myMap.geoObjects.add(myPlacemark);
        myMap.events.add('click', function (e) {
            console.log('hi');
          myMap.balloon.close();
        });
    myMap.geoObjects.add( myCollection ); 
        myMap.setCenter([x, y], 16, {
            checkZoomRange: false
        });
        oldCenter = myMap.getCenter();
        myCollection.add(myPlacemark);
        arrayBalloons.push(myPlacemark);
    });
    $("body, html").animate(function () {
        scrollTo: $(".map-footer").offset().top
    });
    //arrayBalloons[i].balloon.open();
    return false;
  };
  
  function overCenter(x, y) {
    ymaps.ready(function () {
        oldCenter = myMap.getCenter();
        setCenter(x, y);
    });
  };
  function oldPos() {
    ymaps.ready(function () {
        myMap.setCenter(oldCenter);
    });
  };
}  
})();