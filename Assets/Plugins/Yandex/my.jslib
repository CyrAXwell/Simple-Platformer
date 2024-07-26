mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

  ShowFullscreenAdvExtern: function () {
    ysdk.adv.showFullscreenAdv({
        callbacks: {
            onClose: function(wasShown) {
                MyGameInstance.SendMessage('YandexSDK', 'ResumeSound');
            },
            onError: function(error) {
              // some action on error
            }
        }
    })
  },

  ShowRewardedAdvExtern: function () {
    ysdk.adv.showRewardedVideo({
        callbacks: {
            onOpen: () => {
              console.log('Video ad open.');
              MyGameInstance.SendMessage('YandexSDK', 'PausedSound');
            },
            onRewarded: () => {
              console.log('Rewarded!');
              MyGameInstance.SendMessage('YandexSDK', 'OnGetReward');
            },
            onClose: () => {
              console.log('Video ad closed.');
              MyGameInstance.SendMessage('YandexSDK', 'OnCloseAdv');
            }, 
            onError: (e) => {
              console.log('Error while open video ad:', e);
            }
        }
    })
  },

  AddCoinsExtern: function (value) {
    ysdk.getLeaderboards()
          .then(lb => {
            lb.setLeaderboardScore('Coins', value);
            console.log('setLeaderboardScore', value);
          });
  },

  RateGameExtern: function () {
    ysdk.feedback.canReview()
      .then(({ value, reason }) => {
        if (value) {
          ysdk.feedback.requestReview()
            .then(({ feedbackSent }) => {
              console.log(feedbackSent);
              MyGameInstance.SendMessage('YandexSDK', 'HideRateButton');
            })
        } else {
          console.log(reason)
          MyGameInstance.SendMessage('YandexSDK', 'HideRateButton');
      }
    })
  },

  // CheckAuthExtern: function () {
  //   initPlayer().then(_player => {
  //       if (_player.getMode() === 'lite') {
  //         // Игрок не авторизован
  //       } else {
  //         ysdk.feedback.canReview()
  //           .then(({ value, reason }) => {
  //             if (value) {
  //               // Можно оценить
  //             } else {
  //               // Нельзя оценить
  //               console.log(reason);
  //               MyGameInstance.SendMessage('YandexSDK', 'HideRateButton');
  //             }
  //           })
  //       }
  //     }).catch(err => {
  //         // Ошибка при инициализации объекта Player.
  //     });
  // },

  

});