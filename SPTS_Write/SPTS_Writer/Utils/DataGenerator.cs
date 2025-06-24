using SPTS_Writer.Entities;

namespace SPTS_Writer.Utils;
public class DataGenerator
{

    public static List<Question> GenerateSampleQuestions()
    {
        return new List<Question>(){
            new Question() {
                Type = TestMethod.MBTI,
                Detail ="?",
                Options = new List<Option> (){
                    new Option(){
                        Detail = @"Bạn cảm thấy tràn đầy năng lượng khi ở cạnh nhiều người?" ,
                        Value = (AllAnswer) MBTIAnswer.E
                    },
                    new Option(){
                        Detail = @"Khi ở một mình?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                },
            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Trong một nhóm, bạn thường là người nói nhiều?",
                        Value = (AllAnswer) MBTIAnswer.E
                    },
                    new Option(){
                        Detail = @"Lắng nghe nhiều hơn ?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }
            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thích các hoạt động náo nhiệt, tiệc tùng?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Những buổi gặp mặt nhỏ, yên tĩnh ?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thường nói ra suy nghĩ của mình ngay lập tức ?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Nghĩ kỹ rồi mới nói ?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thích giao lưu, kết bạn mới?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Gắn bó với vài người thân thiết?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thấy thoải mái khi thuyết trình?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Cảm thấy lo lắng khi phải nói trước đám đông ?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thích các hoạt động nhóm?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Thích làm việc độc lập?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn dễ dàng làm quen với người lạ ?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Khó mở lòng khi gặp người mới ?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thường hành động rồi mới suy nghĩ?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Suy nghĩ kỹ rồi mới hành động?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn cảm thấy được nạp lại năng lượng khi trò chuyện với người khác ?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Khi ở trong không gian yên tĩnh ?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Khi gặp khó khăn, bạn thường chia sẻ ngay?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Giữ trong lòng và tự suy ngẫm?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn cảm thấy hào hứng trong môi trường sôi động ?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Nhanh mệt khi ở môi trường đó ?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thích thể hiện cảm xúc ra ngoài?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Giữ cảm xúc cho riêng mình ?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn học tốt hơn khi trao đổi trực tiếp ?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Khi tự học một mình ?",
                        Value = (AllAnswer) MBTIAnswer.Iw
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn dễ hòa nhập trong các tình huống xã hội ?",
                        Value = (AllAnswer) MBTIAnswer.E 
                    },
                    new Option(){
                        Detail = @"Cần thời gian để thích nghi?",
                        Value = (AllAnswer) MBTIAnswer.I
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn tin vào kinh nghiệm thực tế?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Tin vào cảm nhận và linh cảm?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Khi học một khái niệm mới, bạn cần ví dụ cụ thể?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Bạn hiểu nhanh thông qua lý thuyết?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn quan tâm đến chi tiết ?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Quan tâm đến bức tranh tổng thể ?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn chú trọng hiện tại và thực tế?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Suy nghĩ về tương lai và khả năng ?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn tin vào ""thấy mới tin"" ?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Tin vào những gì có thể xảy ra?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn nhớ thông tin theo thứ tự, mốc thời gian?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Theo ý tưởng và liên kết ?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Khi giải quyết vấn đề, bạn tập trung vào hiện trạng?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Tìm hướng đi mới, sáng tạo?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thích làm việc có quy trình rõ ràng?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Thích sáng tạo, linh hoạt ?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn dễ nhớ dữ kiện cụ thể ?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Dễ nhớ ý tưởng, ẩn dụ ?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thích hướng dẫn từng bước cụ thể?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Muốn hiểu tổng thể để tự làm?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn đánh giá cao sự ổn định?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Đam mê thay đổi và phát triển?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thấy dễ hiểu hơn khi có ví dụ thực tế?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Khi có mô hình và khái niệm trừu tượng?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn chú ý vào chi tiết nhỏ trong mọi việc ?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Nhìn nhận vấn đề ở tầm vĩ mô?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn cảm thấy an tâm khi tuân theo chuẩn mực đã có ?",
                        Value = (AllAnswer) MBTIAnswer.S 
                    },
                    new Option(){
                        Detail = @"Thích thách thức chuẩn mực ?",
                        Value = (AllAnswer) MBTIAnswer.N
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thường hỏi “nó là gì ?",
                        Value = (AllAnswer) MBTIAnswer.() 
                    },
                    new Option(){
                        Detail = @"“nó có thể trở thành gì?",
                        Value = (AllAnswer) MBTIAnswer.()
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Khi ra quyết định, bạn ưu tiên lý trí ?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Ưu tiên cảm xúc, mối quan hệ?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn tin rằng sự thật là quan trọng hơn cảm xúc?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Cảm xúc quan trọng ngang với sự thật ?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn phê bình người khác một cách thẳng thắn ?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Dịu dàng và tế nhị hơn?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn ưu tiên công việc hiệu quả hơn cảm giác người khác ?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Cân bằng giữa cả hai ?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn dễ tranh luận để bảo vệ quan điểm ?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Tránh tranh cãi vì không muốn làm mất lòng ?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn có khuynh hướng phân tích lý lẽ?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Đặt mình vào cảm xúc của người khác ?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn đánh giá cao sự công bằng ?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Đánh giá cao sự quan tâm, đồng cảm ?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Trong nhóm, bạn là người ra quyết định?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Người hòa giải?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thích sự thẳng thắn?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Thích nhẹ nhàng, tế nhị ?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Khi làm việc, bạn tập trung vào mục tiêu?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Tập trung vào con người?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn dễ chấp nhận các quyết định dựa trên logic ?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Dựa trên cảm nhận và lòng tốt ?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Khi gặp mâu thuẫn, bạn tìm lý do khách quan?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Cố gắng hiểu cảm xúc đôi bên?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thích phân tích ?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Thích cảm nhận và sẻ chia?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn cảm thấy ổn khi người khác không đồng tình ?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Cảm thấy khó chịu nếu ai đó buồn vì bạn?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn làm theo đúng quy định, nguyên tắc?",
                        Value = (AllAnswer) MBTIAnswer.T 
                    },
                    new Option(){
                        Detail = @"Linh hoạt theo từng người, từng hoàn cảnh?",
                        Value = (AllAnswer) MBTIAnswer.F
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thích lập kế hoạch trước ?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Thích làm mọi thứ theo cảm hứng ?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn cảm thấy thoải mái khi công việc theo lịch trình ?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Khi mọi thứ có thể thay đổi ?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn hay hoàn thành mọi việc sớm ?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Hay để gần deadline mới làm?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn không thích thay đổi kế hoạch đột ngột?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Dễ thích nghi với thay đổi ?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thích có danh sách “to -do”?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Làm việc tùy theo ưu tiên mỗi lúc ?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn cảm thấy hài lòng khi hoàn thành nhiệm vụ?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Khi được khám phá điều mới ?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thường lên kế hoạch kỹ cho việc học ?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Học theo cảm hứng?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn có xu hướng đánh giá nhanh tình huống?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Muốn xem xét từ nhiều góc độ trước khi kết luận?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thấy mệt khi phải thay đổi liên tục?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Thích cảm giác linh hoạt và tự do?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thích làm việc từng bước cụ thể ?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Làm nhiều việc cùng lúc ?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thấy vui khi mọi thứ rõ ràng?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Khi mọi thứ mở và còn có thể thay đổi ?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn lên kế hoạch trước cho cả kỳ nghỉ ?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Đi đâu thấy thú vị thì đi ?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn thường kết thúc công việc trước thời hạn ?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Hay nước đến chân mới nhảy?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn chọn cách ổn định lâu dài?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Luôn tìm cơ hội mới?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            },
            new Question(){
                Type = TestMethod.MBTI,
                Detail = "?",
                Options = new List<Option>(){
                    new Option(){
                        Detail = @"Bạn muốn kiểm soát mọi việc ?",
                        Value = (AllAnswer) MBTIAnswer.J 
                    },
                    new Option(){
                        Detail = @"Không ngại sự bất định, mạo hiểm?",
                        Value = (AllAnswer) MBTIAnswer.P
                    }
                }

            }
        }
    }

    public static List<Test> GenerateSampleTests(List<Question> allQuestions)
    {
        var tests = new List<Test>();
        var random = new Random();
        var testMethods = Enum.GetValues(typeof(TestMethod)).Cast<TestMethod>().ToList();
        foreach (var method in testMethods)
        {
            // Filter questions by method
            var methodQuestions = allQuestions.Where(q => q.Type == method).ToList();

            for (int i = 0; i < 2; i++)
            {
                // Randomly select questions for the test
                var selectedQuestions = methodQuestions.OrderBy(q => random.Next()).Take(10).ToList(); // Example: 30 questions per test

                tests.Add(new Test
                {
                    Id = Guid.NewGuid(), // Assuming Base has Id
                    Method = method,
                    Author = $"Author_{method}_{i + 1}",
                    TestDate = DateTime.UtcNow.AddDays(-random.Next(100)),
                    NumberOfQuestions = selectedQuestions.Count,
                    Questions = selectedQuestions
                });
            }
        }

        return tests;
    }


